using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

// ByteCodeParser takes a bytecode buffer and iterates through instructions.
//
// The main use-case is to process an entire function at at time. It handles
// this by detecting the farthest branch target as it iterates so that it stops
// on the last return instruction. It can also be given an arbitrary start
// position and length so that it can process an entire script's bytecode buffer
// at once without caring about function boundaries. This is also used to parse
// the gaps between functions to see if they contain unreachable functions.
//
// All SCI versions are supported, but the client has to tell ByteCodeParser the
// version and endinaness. There is no auto-detection. I kept thinking I'd need
// to auto-detect this, but by the time you've parsed a compiled script resource
// far enough to find the bytecode you already know the endianness and which
// high-level version you're dealing with. (Endianness is passed via Span.)
//
// ByteCodeParser does not interpret any operands except branch targets.

namespace SCI.Resource
{
    [Flags]
    public enum ByteCodeParserStatus
    {
        Ok = 0,
        UnknownOpcode = 1,        // a dummy opcode was reached
        TruncatedInstruction = 2, // a truncated instruction was reached, ending the parse loop.
        OutOfBoundsBranch = 4,    // a jmp/bnt/bt with an OOB target was reached
        NoReturn = 8              // the parse loop ended without the final ret when StopOnFinalReturn was set
    }

    public class ByteCodeParser
    {
        public Span Buffer { get; private set; }
        public int StartPosition { get; private set; }
        public int EndPosition { get; private set; }
        public ByteCodeVersion Version { get; private set; }
        public IReadOnlyList<Opcode> Opcodes { get; private set; }
        public bool StopOnFinalReturn { get; set; }

        int maxBranchTarget;
        bool endReached;

        public int Position { get; private set; }
        public int NextInstructionPosition { get; private set; }
        public Opcode Opcode { get; private set; }
        public Operation Operation { get { return Opcode.Operation; } }
        public ReadOnlyCollection<byte> Operands { get { return Opcode.Operands; } }
        public ByteCodeParserStatus Status { get; set; } // allows resetting

        public ByteCodeParser() { }

        public ByteCodeParser(Span buffer, int startPosition, int endPosition, ByteCodeVersion version)
        {
            Parse(buffer, startPosition, endPosition, version);
        }

        public ByteCodeParser(Function function)
        {
            Parse(function);
        }

        public void Parse(Span buffer, int startPosition, int endPosition, ByteCodeVersion version)
        {
            Buffer = buffer;
            StartPosition = startPosition;
            EndPosition = endPosition;
            Version = version;
            Opcodes = Opcode.GetOpcodeSet(Version);
            StopOnFinalReturn = true;
            maxBranchTarget = 0;
            endReached = false;
            NextInstructionPosition = StartPosition;
            Status = ByteCodeParserStatus.Ok;
        }

        public void Parse(Function function)
        {
            Parse(function.Code, 0, function.Code.Length, function.Script.Game.ByteCodeVersion);
        }

        public bool Next()
        {
            // update current position
            Position = NextInstructionPosition;

            if (Position < EndPosition && !endReached)
            {
                // get opcode from table
                Opcode = Opcodes[Buffer[Position]];
                if (Operation == Operation.unused)
                {
                    Status |= ByteCodeParserStatus.UnknownOpcode;
                }

                // calculate next instruction position
                NextInstructionPosition += GetInstructionLength();

                // validate that instruction isn't longer than the buffer
                if (NextInstructionPosition > EndPosition)
                {
                    Status |= ByteCodeParserStatus.TruncatedInstruction;
                    endReached = true;
                    return false;
                }

                // track the maximum branch target and automatically stop on the final ret.
                // optional so that the parser can be thrown at all the bytecode for a script.
                // you wouldn't think that would work, but it does because buffer bytes are 0
                // so they just get parsed as single-byte bnot instructions until the next
                // real instruction is reached.
                switch (Operation)
                {
                    case Operation.bt:
                    case Operation.bnt:
                    case Operation.jmp:
                        {
                            int branchTarget = GetBranchTarget();
                            if (StartPosition <= branchTarget && branchTarget < EndPosition)
                            {
                                maxBranchTarget = Math.Max(maxBranchTarget, branchTarget);
                            }
                            else
                            {
                                Status |= ByteCodeParserStatus.OutOfBoundsBranch;
                            }
                        }
                        break;

                    case Operation.ret:
                        if (StopOnFinalReturn)
                        {
                            if (Position >= maxBranchTarget)
                            {
                                endReached = true;
                            }
                        }
                        break;
                }

                return true;
            }

            // if we've reached the end of the buffer and were supposed to stop
            // on the final return but didn't reach it, then indicate that.
            if (Position == EndPosition && StopOnFinalReturn && !endReached)
            {
                Status |= ByteCodeParserStatus.NoReturn;
            }

            return false;
        }

        public byte GetInstructionLength()
        {
            // "file" requires counting characters.
            // use pre-calculated length for everyone else.
            if (Opcode.Operation == Operation.file)
            {
                return (byte)(1 + GetOperandLength(0));
            }
            else
            {
                return Opcode.Length;
            }
        }

        public int GetOperandPosition(int index)
        {
            int position = Position + 1;
            for (int i = 0; i < index; ++i)
            {
                position += Opcode.Operands[i];
            }
            return position;
        }

        public byte GetOperandLength(int index)
        {
            byte operandSize = Opcode.Operands[index];
            if (operandSize == 0)
            {
                // "file" requires counting characters
                byte length = 1;
                while (Buffer[Position + length] != 0)
                {
                    length++;
                }
                return length;
            }
            else
            {
                return operandSize;
            }
        }

        // returns the operand as an unsigned word.
        public UInt16 GetOperand(int index)
        {
            return GetOperand(index, false);
        }

        // returns the operand as a signed word.
        // if the operand is a byte then it is sign extended.
        public Int16 GetSignedOperand(int index)
        {
            return (Int16)GetOperand(index, true);
        }

        public UInt16 GetOperand(int index, bool signExtend)
        {
            int position = Position + 1; // skip opcode byte
            for (int i = 0; i < index; ++i)
            {
                position += Opcode.Operands[i];
            }

            if (Opcode.Operands[index] == 1)
            {
                if (signExtend)
                {
                    return (UInt16)((Buffer[position] >= 0x80) ? (Buffer[position] | 0xff00) : Buffer[position]);
                }
                else
                {
                    return Buffer[position];
                }
            }
            else if (Opcode.Operands[index] == 2)
            {
                // endianness handled by the Span
                return Buffer.GetUInt16(position);
            }
            else
            {
                // fool
                throw new Exception("GetOperand() called on: " + Opcode);
            }
        }

        public string GetStringOperand()
        {
            if (Opcode.Operation == Operation.file)
            {
                var filename = new StringBuilder();
                int position = Position + 1; // opcode
                while (Buffer[position] != 0)
                {
                    filename.Append((char)Buffer[position++]);
                }
                return filename.ToString();
            }
            else
            {
                throw new Exception("GetStringOperand() called on: " + Opcode);
            }
        }

        public bool IsBranch()
        {
            switch (Opcode.Operation)
            {
                case Operation.bt:
                case Operation.bnt:
                case Operation.jmp:
                    return true;
                default:
                    return false;
            }
        }

        public int GetBranchTarget()
        {
            if (!(IsBranch())) throw new Exception("Instruction isn't a branch");

            return Position + GetInstructionLength() + GetSignedOperand(0);
        }

        // convenience method to stop while (parser.Next()) loops where break is inconvenient
        public void Stop()
        {
            Position = EndPosition;
        }
    }
}
