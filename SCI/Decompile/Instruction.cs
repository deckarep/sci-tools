using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SCI.Resource;

// Instruction and InstructionList

namespace SCI.Decompile
{
    [Flags]
    enum InstructionFlag
    {
        None = 0,
        Break            = (1<< 1), // jmp is a Break, bt is a BreakIf
        Continue         = (1<< 2), // jmp is a Continue, bt is a ContinueIf
        CompilerBug      = (1<< 3), // Broken instruction; decompiler will annotate this
        DefangedBranch   = (1<< 4), // Don't treat this like a branch anymore; IsBranch returns false
        TrustMeItsAFor   = (1<< 5), // Workarounds use this to tag known "exotic" for loops
        MathAssignment   = (1<< 6), // Treat arithmetic instruction as arithmetic assignment
        Inconsumable     = (1<< 7), // Arithmetic assignment doesn't write to acc (compiler bug)

        // Flags that are only used for graphviz coloring
        Deoptimized      = (1<<26), // Deoptimized instruction (branch)
        DeoptimizedLoop  = (1<<27), // Deoptimized instruction in a loop (branch)
        DeoptimizedGraph = (1<<28), // Deoptimized instruction using graph techniques
        LoopCondition    = (1<<29), // Bnt that is believed to be the end of the loop condition
        ThirdPartyBranch = (1<<30), // Companion/Studio branch i fixed up for my CFA algorithm
    }

    enum VariableType { None, Global, Local, Parameter, Temp }

    class Instruction
    {
        public int Position; // relative to start of function
        public int AbsolutePosition; // position in the script resource
        public byte Length;
        public Operation Operation;
        public int[] Parameters; // 32-bit to handle SCI3 relocated values
        public InstructionFlag Flags;
        public int LoopLevel; // used by exotic breaks and continues

        public bool IsBranch
        {
            get
            {
                // Once an instruction is identified as a Continue(If)
                // or Break(If), it is no longer treated as a branch.
                switch (Operation)
                {
                    case Operation.bt:
                    case Operation.bnt:
                    case Operation.jmp:
                        return (Flags & (InstructionFlag.Continue |
                                         InstructionFlag.Break |
                                         InstructionFlag.DefangedBranch)) == InstructionFlag.None;
                    default:
                        return false;
                }
            }
        }

        public int BranchTarget
        {
            get
            {
                if (IsBranch)
                    return Position + Length + Parameters[0];
                else
                    throw new Exception("not a branch");
            }
            set // for patching
            {
                if (IsBranch)
                    Parameters[0] = value - (Position + Length);
                else
                    throw new Exception("not a branch");
            }
        }

        public int PopStackAmount
        {
            get
            {
                if (Operation.GetFlags().HasFlag(OpFlags.PopsStack))
                {
                    switch (Operation)
                    {
                        case Operation.call:
                        case Operation.callk:
                        case Operation.callb:
                        case Operation.calle:
                            // last parameter divided by two plus one (first push is parameter count)
                            return Parameters[Parameters.Length - 1] / 2 + 1;
                        case Operation.send:
                        case Operation.self:
                        case Operation.super:
                            // last parameter divided by two
                            return Parameters[Parameters.Length - 1] / 2;
                        default:
                            // all the other poppers
                            return 1;
                    }
                }
                else
                {
                    throw new Exception("not a stack pop");
                }
            }
        }

        // returns the type of variable that the instruction operates on.
        // returns None if this isn't a variable instruction.
        public VariableType GetVariableType()
        {
            if (Operation.lag <= Operation && Operation <= Operation.minusspi)
            {
                switch ((Operation - Operation.lag) % 4)
                {
                    case 0:  return VariableType.Global;
                    case 1:  return VariableType.Local;
                    case 2:  return VariableType.Temp;
                    default: return VariableType.Parameter;
                }
            }
            else if (Operation == Operation.lea)
            {
                switch (Parameters[0] & 6)
                {
                    case 0:  return VariableType.Global;
                    case 2:  return VariableType.Local;
                    case 4:  return VariableType.Temp;
                    default: return VariableType.Parameter;
                }
            }
            return VariableType.None;
        }

        public int GetVariableIndex()
        {
            if (Operation.lag <= Operation && Operation <= Operation.minusspi)
            {
                return Parameters[0];
            }
            else if (Operation == Operation.lea)
            {
                return Parameters[1];
            }
            else
            {
                throw new Exception("not a variable instruction");
            }
        }

        // returns true if the instruction uses an index to a variable.
        public bool IsComplexVariable()
        {
            if (Operation.lag <= Operation && Operation <= Operation.minusspi)
            {
                return (((Operation - Operation.lag) % 16) >= 8);
            }
            else if (Operation == Operation.lea)
            {
                return (Parameters[0] & 0x10) != 0;
            }
            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0:X4} {1}", Position, Operation.GetName());
            if (Parameters != null)
            {
                foreach (var p in Parameters)
                {
                    sb.AppendFormat(" {0:X4}", (UInt16)p);
                }
            }
            return sb.ToString();
        }

        LinkedListNode<Instruction> node;
        public void SetNode(LinkedListNode<Instruction> node) { this.node = node; }
        public Instruction Next { get { return node.Next?.Value; } }
        public Instruction Prev { get { return node.Previous?.Value; } }

        public Instruction GetNext(Operation operation, Instruction stopAt = null)
        {
            var myNode = node;
            while ((myNode = myNode.Next) != null && myNode.Value.Operation != operation)
            {
                if (myNode.Value == stopAt) return null;
            }
            return myNode?.Value;
        }

        public Instruction GetPrev(Operation operation, Instruction stopAt = null)
        {
            var myNode = node;
            while ((myNode = myNode.Previous) != null && myNode.Value.Operation != operation)
            {
                if (myNode.Value == stopAt) return null;
            }
            return myNode?.Value;
        }

        public bool HasFlag(InstructionFlag flag)
        {
            return Flags.HasFlag(flag);
        }
    }

    class InstructionList : IEnumerable<Instruction>
    {
        LinkedList<Instruction> list = new LinkedList<Instruction>();
        Dictionary<int, LinkedListNode<Instruction>> index = new Dictionary<int, LinkedListNode<Instruction>>();

        public int Count { get { return list.Count; } }
        public Instruction First { get { return list.First?.Value; } }
        public Instruction Last { get { return list.Last?.Value; } }

        public Function Function { get; set; }

        // string operand from the file debug instruction
        public string DebugFileName { get; private set; } = "";

        public void Add(Instruction i)
        {
            var node = list.AddLast(i);
            index.Add(i.Position, node);
            i.SetNode(node);
        }

        public void Remove(Instruction i)
        {
            Remove(i.Position);
        }

        public void Remove(int position)
        {
            var node = index[position];
            node.Value.SetNode(null);
            list.Remove(node);
            index.Remove(position);
        }

        public Instruction this[int position] { get { return index[position].Value; } }
        public bool Has(int position) { return index.ContainsKey(position); }

        public IEnumerator<Instruction> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        // Build an InstructionList from a function by using ByteCodeParser.
        // Handles signed vs unsigned immediate values, they are now probably correct.
        // Handles branch operands, they are still signed relative offsets.
        // Handles lofsa operands, their values are now the absolute object position.
        // Handles call addresses, they are now the absolute function position.
        // Handles LSCI ID operands, they are now absolute offsets to objects and code.
        public static InstructionList Parse(Function function)
        {
            var instructions = new InstructionList();
            instructions.Function = function;
            var parser = new ByteCodeParser(function);
            while (parser.Next())
            {
                var instruction = new Instruction();
                instruction.Position = parser.Position;
                instruction.AbsolutePosition = parser.Position + (int)function.CodePosition;
                instruction.Length = parser.GetInstructionLength();
                instruction.Operation = parser.Operation;
                instruction.Parameters = new int[parser.Opcode.Operands.Count];
                for (int i = 0; i < instruction.Parameters.Length; ++i)
                {
                    if (i == 0 && (instruction.IsBranch || instruction.Operation == Operation.call))
                    {
                        instruction.Parameters[i] = parser.GetSignedOperand(i);
                        if (instruction.Operation == Operation.call)
                        {
                            // in LSCI, the first call operand is a block index instead of an offset.
                            // resolve the absolute position with relocations.
                            // after this, the call instruction can be treated like normal (non-LSCI)
                            UInt32 operandPosition = (UInt32)(function.CodePosition + parser.GetOperandPosition(i));
                            UInt32 realOperand;
                            if (function.Script.Relocations.TryGetValue(operandPosition, out realOperand))
                            {
                                instruction.Parameters[i] = (int)realOperand;
                            }
                            else
                            {
                                // set call parameter to the absolute position of the function being called
                                instruction.Parameters[i] += parser.NextInstructionPosition + (int)function.CodePosition;
                            }
                        }
                    }
                    else if (instruction.Operation == Operation.lofsa ||
                             instruction.Operation == Operation.lofss)
                    {
                        if (function.Script.Game.LofsAddressType == LofsAddressType.Relative)
                        {
                            instruction.Parameters[i] = parser.GetSignedOperand(i) + parser.NextInstructionPosition + (int)function.CodePosition;
                        }
                        else if (function.Script.Game.LofsAddressType == LofsAddressType.Absolute)
                        {
                            instruction.Parameters[i] = parser.GetOperand(i);
                        }
                        else if (function.Script.Game.LofsAddressType == LofsAddressType.Relocated)
                        {
                            // lofsa and lofss are zero in the bytecode, the real
                            // value is always in the relocations and may be 32-bit.
                            UInt32 operandPosition = (UInt32)(function.CodePosition + parser.GetOperandPosition(i));
                            instruction.Parameters[i] = (int)function.Script.Relocations[operandPosition];
                        }
                        else throw new Exception("unknown lofs type");
                    }
                    else if (instruction.Operation == Operation.loadID ||
                             instruction.Operation == Operation.pushID)
                    {
                        // LSCI loadID and pushID take a block index to an object.
                        // resolve the absolute position with relocations.
                        // after this, these two opcodes can be treated like lofsa  and lofss.
                        UInt32 operandPosition = (UInt32)(function.CodePosition + parser.GetOperandPosition(i));
                        instruction.Parameters[i] = (int)function.Script.Relocations[operandPosition];
                    }
                    else if (instruction.Operation == Operation.file)
                    {
                        // GetOperand() can't be called on file, and Instruction
                        // doesn't care about the file operand. file and line
                        // end up stripped from InstructionList early on.
                        // That said, all instructions appear in asm blocks,
                        // so store the string here in the instruction list.
                        // file only appears once at the start of a function.
                        instructions.DebugFileName = parser.GetStringOperand();
                    }
                    else if (instruction.Operation == Operation.ldi ||
                             instruction.Operation == Operation.pushi)
                    {
                        // Immediate integer values (pushi/ldi) can be signed or
                        // unsigned when the wide opcode is used; I have no way of
                        // knowing which value was entered into source code.
                        // Example: pushi ffff could have been -1 or 65535 or $ffff.
                        // Annotators can come along later and reformat some things
                        // from context, but it's important to make a good guess as
                        // early in the process as possible.
                        // Also, at this point we still know the instruction size,
                        // and that matters because pushi ff can only mean -1.
                        if (parser.GetOperandLength(i) == 1)
                        {
                            // 8-bit operand, we know this is signed
                            Int16 value = parser.GetSignedOperand(i);
                            instruction.Parameters[i] = value;
                        }
                        else
                        {
                            // 16-bit operand, we can only guess, but i have an okay one.
                            // it also gets used on local variable values.
                            UInt16 raw = parser.GetOperand(i);
                            int value = raw.GetSignedOrUnsigned(function.Script.Game.ByteCodeVersion);
                            instruction.Parameters[i] = value;
                        }
                    }
                    else
                    {
                        instruction.Parameters[i] = parser.GetOperand(i);
                    }
                }
                instructions.Add(instruction);
            }
            return instructions;
        }
    }
}
