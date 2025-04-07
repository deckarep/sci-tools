using System;
using System.Collections.Generic;
using System.Linq;

// Decompiler AST. Every Node is an expression in a function.
//
// This AST only represents the body of a function.
// It is not necessary, or desirable, to AST the entire script file.
// I'm not consuming script files, just outputting them.
// Everything outside the function body is modeled in simple classes.
//
// Every node type has an enum, but only some have their own class.
// If they all had their own class, that would have been a lot of
// classes and a lot of boilerplate, and it wouldn't have helped.
// Some classes only exist to store associated values that are part
// of an underlying instruction and can't be represented by nodes.
// Like Number or String values, or KernelCall function numbers/names.
// Others only exist to help me, by creating getter properties that
// document which child nodes represent what, and making the code
// that reads these values clearer. The getters also make debugging
// easier. For example, the If class' Test/Then/[Else] properties.
//
// The AST participates in a simple depth first visitor pattern.

namespace SCI.Decompile.Ast
{
    enum NodeType
    {
        // default is just a list of expressions (nodes)
        List,

        // literals
        Number, String, Said, Selector,

        // identifiers (class, lofsa, lofss)
        Class, Object,

        // built-in identifiers
        Self, Super, Rest, Info,

        // variable / property
        Variable, ComplexVariable, Property,

        // return statement (can have zero or one child)
        Return,

        // variable / property manipulation
        Assignment, Increment, Decrement,

        AssignmentAdd, AssignmentSub, AssignmentMul, AssignmentDiv,
        AssignmentShl, AssignmentShr, AssignmentXor,
        AssignmentBinAnd, AssignmentBinOr,

        // address-of (variables / complex variables)
        AddressOf,

        // unary
        Not, BinNot, Neg,

        // math / logic (binary or n-ary)
        Add, Sub, Mul, Div, Mod, Shl, Shr, Xor, BinAnd, BinOr, And, Or,

        // compare (n-ary)
        Eq, Ne, Gt, Ge, Ugt, Uge, Lt, Le, Ult, Ule,

        // control flow structures
        If, Switch, Cond, Case, Else, // (Else is for Switch/Cond, not If)

        // loops
        Loop, Break, Continue, BreakIf, ContinueIf,

        // send / super / self
        Send, SendMessage,

        // function calls
        KernelCall, PublicCall, LocalCall,

        // pseudo (won't appear in the final AST)
        Prev, // pushed to stack by pprev to be consumed by a comparison
    }

    [Flags]
    enum NodeFlags
    {
        None = 0,
        CompilerBug = 1,
        Copy = 2,
    }

    class Node
    {
        public Node(NodeType type) { Type = type; }
        public Node(NodeType type, params Node[] children) { Type = type; Add(children); }

        public NodeType Type { get; private set; }
        List<Node> children = new List<Node>();
        public IReadOnlyList<Node> Children { get { return children; } }
        public Node Parent { get; private set; }
        public NodeFlags Flags { get; set; }

        // TextLengthVisitor calculates this for every node.
        // If this node and its children can be rendered on one line,
        // then this is the length of that string. Otherwise, -1.
        // This value is only to be used by FunctionWriter for
        // pretty-print decisions; it may be a lie to influence those
        // decisions. The actual text could be a different length.
        public int TextLength { get; set; }

        // Adds child nodes. Or just one. Or none. Sets the Parent.
        public void Add(params Node[] nodes)
        {
            foreach (var node in nodes)
            {
                if (node != null)
                {
                    node.Parent = this;
                    children.Add(node);
                }
            }
        }

        // Add a child node at a specific position
        public void Insert(int index, Node node)
        {
            node.Parent = this;
            children.Insert(index, node);
        }

        // Remove a child node. Clears the Parent.
        public void Remove(Node node) { node.Parent = null; children.Remove(node);  }
        public void Remove(int index) { children[index].Parent = null; children.RemoveAt(index); }

        // Replaces a child node. Sets and clears the Parent.
        public void Replace(Node oldNode, Node newNode)
        {
            int index = children.IndexOf(oldNode);
            newNode.Parent = this;
            oldNode.Parent = null;
            children[index] = newNode;
        }

        public Node Next
        {
            get
            {
                int nextIndex = Parent.Children.IndexOf(this) + 1;
                if (nextIndex == 0) throw new Exception("Parent doesn't contain this node");
                if (nextIndex == Parent.Children.Count) return null;
                return Parent.Children[nextIndex];
            }
        }

        public Node Prev
        {
            get
            {
                int prevIndex = Parent.Children.IndexOf(this) - 1;
                if (prevIndex == -2) throw new Exception("Parent doesn't contain this node");
                if (prevIndex == -1) return null;
                return Parent.Children[prevIndex];
            }
        }

        public override string ToString() { return Type.ToString(); }

        public virtual Node Copy() { throw new Exception("Node copying not supported by: " + Type); }

        public virtual void Accept(Visitor visitor)
        {
            AcceptChildren(visitor);
            visitor.Visit(this);
        }

        protected void AcceptChildren(Visitor visitor)
        {
            // for instead of foreach so visitor can replace current node
            for (int i = 0; i < Children.Count; i++)
            {
                Node child = Children[i];
                child.Accept(visitor);
                if (i >= Children.Count || child != Children[i])
                {
                    // something changed!
                    // maybe the child replaced themselves or
                    // deleted a previous child.
                    int newIndex = Children.IndexOf(child);
                    if (newIndex == -1)
                    {
                        // the child is gone. lower index so that
                        // the new node in its place gets scanned.
                        i--;
                    }
                    else if (newIndex < i)
                    {
                        // some nodes prior to the child disappeared.
                        // adjust index so that other nodes don't get skipped.
                        // (this could also mean the child moved up,
                        // but i didn't write any annotators like that)
                        i = newIndex;
                    }
                }
            }
        }

        // Recursive node equality test, not to be confused with Equals() and friends
        public virtual bool Same(Node n)
        {
            if (Type == n.Type && Children.Count == n.Children.Count)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    if (!Children[i].Same(n.Children[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }

    // LITERALS

    class Number : Node
    {
        public Number(int value, bool isHex = false) : base(NodeType.Number) { Value = value; IsHex = isHex; }
        public int Value { get; set; }
        public bool IsHex { get; set; }
        public override string ToString() { return (!IsHex) ? Value.ToString() : ("$" + ((UInt16)Value).ToString("x4")); }
        public override Node Copy() { var n = new Number(Value, IsHex); n.Flags |= NodeFlags.Copy; return n; }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Value == ((Number)n).Value;
        }
    }

    class String : Node
    {
        public String(string value) : base(NodeType.String) { Value = value; }
        public string Value { get; set; }
        public override string ToString() { return "\"" + Value + "\""; }
        public override Node Copy() { var n = new String(Value); n.Flags |= NodeFlags.Copy; return n; }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Value == ((String)n).Value;
        }
    }

    class Said : Node
    {
        public Said(string value) : base(NodeType.Said) { Value = value; }
        public string Value { get; set; }
        public override string ToString() { return "'" + Value + "'"; }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Value == ((Said)n).Value;
        }
    }

    class Selector : Node
    {
        public Selector(string name, int value) : base(NodeType.Selector) { Name = name; Value = value; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsLiteral { get; set; } // when set to true, is output with a "#"
        public override string ToString() { return (IsLiteral ? ("#" + Name) : Name); }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Value == ((Selector)n).Value;
        }
    }

    // IDENTIFIERS

    class Class : Node
    {
        public Class(string name, int number) : base(NodeType.Class) { Name = name; Number = number; }
        public string Name { get; set; }
        public int Number { get; set; } // meta information if name not available
        public override string ToString() { return "Class: " + Name; }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Number == ((Class)n).Number;
        }
    }

    class Obj : Node
    {
        public Obj(string name, int offset) : base(NodeType.Object) { Name = name; Offset = offset; }
        public string Name { get; set; }
        public int Offset { get; set; } // meta information if name not available
        public override string ToString() { return "Object: " + Name; }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Offset == ((Obj)n).Offset;
        }
    }

    // VARIABLES AND PROPERTIES

    class Variable : Node
    {
        public Variable(VariableType type, int index) : base(NodeType.Variable) { VariableType = type; Index = index; }
        public VariableType VariableType { get; set; }
        public int Index { get; private set; }
        public override string ToString() { return VariableType.ToString() + " " + Index; }
        public override Node Copy() { var n = new Variable(VariableType, Index); n.Flags |= NodeFlags.Copy; return n; }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && VariableType == ((Variable)n).VariableType && Index == ((Variable)n).Index;
        }
    }

    // array access, read and write
    class ComplexVariable : Node
    {
        public ComplexVariable(Variable variable, Node index) : base(NodeType.ComplexVariable, variable, index) { }
        public Variable Variable { get { return (Variable)Children[0]; } }
        public Node Index { get { return Children[1]; } }
        public override string ToString() { return "[" + Variable + "]"; }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class Property : Node
    {
        public Property(string name, int index) : base(NodeType.Property) { Name = name; Index = index; }
        public string Name { get; set; }
        public int Index { get; set; }
        public override string ToString() { return "Property: " + Name; }
        public override Node Copy() { var n = new Property(Name, Index); n.Flags |= NodeFlags.Copy; return n; }

        public override void Accept(Visitor visitor) { visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Index == ((Property)n).Index;
        }
    }

    // ASSIGNMENT: variables and properties

    class Assignment : Node
    {
        public Assignment(Node dest, Node source) : base(NodeType.Assignment, dest, source) { }
        public Node Dest { get { return Children[0]; } }
        public Node Source { get { return Children[1]; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class Increment : Node
    {
        public Increment(Node operand) : base(NodeType.Increment, operand) { }
        public Node Operand { get { return Children[0]; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class Decrement : Node
    {
        public Decrement(Node operand) : base(NodeType.Decrement, operand) { }
        public Node Operand { get { return Children[0]; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    // ADDRESS OF: variables

    class AddressOf : Node
    {
        public AddressOf(Variable variable) : base(NodeType.AddressOf, variable) { }
        public AddressOf(ComplexVariable variable) : base(NodeType.AddressOf, variable) { }
        public Node Operand { get { return Children[0]; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    // COMPARE

    class Compare : Node
    {
        public Compare(NodeType type, params Node[] children) : base(type, children) { }
        public Node Left  { get { return Children[0]; } }
        public Node Right { get { return Children[1]; } }
        // there could be more

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    // CONTROL FLOW STRUCTURES

    class If : Node
    {
        public If(Node test, Node then, Node else_ = null) : base(NodeType.If, test, then) { if (else_ != null) { Add(else_); } }
        public Node Test { get { return Children[0]; } }
        public Node Then { get { return Children[1]; } }
        public Node Else { get { return (Children.Count > 2) ? Children[2] : null; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class Switch : Node
    {
        public Switch() : base (NodeType.Switch) { }
        public Node Head { get { return Children[0]; } }
        public IEnumerable<Case> Cases { get { return Children.Skip(1).TakeWhile(n => n is Case).Select(n => (Case)n); } }
        public Else Else { get { return Children.LastOrDefault(n => n.Type == NodeType.Else) as Else; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class Cond : Node
    {
        public Cond() : base (NodeType.Cond) { }
        public IEnumerable<Case> Cases { get { return Children.TakeWhile(n => n is Case).Select(n => (Case)n); } }
        public Else Else { get { return Children.LastOrDefault(n => n.Type == NodeType.Else) as Else; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class Case : Node
    {
        public Case() : base (NodeType.Case) { }
        public Node Test { get { return Children[0]; } }
        public Node Body { get { return Children[1]; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class Else : Node // Switch/Cond Else; If's Else is just a List as its third child.
    {
        public Else() : base (NodeType.Else) { }
        public Node Body { get { return Children[0]; } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    // LOOPS

    // order of nodes doesn't reflect execution order, unlike everywhere else in this AST.
    // this order lets me use one node type for all three kinds of loops.
    class Loop : Node
    {
        public Loop() : base (NodeType.Loop) { }
        public Node Body { get { return Children[0]; } } // everybody has a body
        public Node Test { get { return Children.Count > 1 ? Children[1] : null; } } // for and while
        public Node ForInit { get { return Children.Count > 2 ? Children[2] : null; } } // for
        public Node ForReinit { get { return Children.Count > 3 ? Children[3] : null; } } // for

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    // SENDS [ send / self / super ]

    class Send : Node
    {
        public Send(Node receiver, params SendMessage[] children) : base (NodeType.Send) { Add(receiver); Add(children); }
        public Node Receiver { get { return Children[0]; } }
        public IEnumerable<Node> Messages { get { return Children.Skip(1); } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    class SendMessage : Node // couldn't come up with a better term for a selector+parameter-list pair
    {
        public SendMessage(Node selector, params Node[] parameters ) : base (NodeType.SendMessage) { Add(selector); Add(parameters); }
        public Node Selector { get { return Children[0]; } } // has to be a node because it can be dynamic, like in (Eval) procedure
        public IEnumerable<Node> Parameters { get { return Children.Skip(1); } }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
    }

    // FUNCTION CALLS

    class KernelCall : Node
    {
        public KernelCall(string name, int function, params Node[] children) :
            base (NodeType.KernelCall) { Name = name; Function = function; Add(children); }
        public string Name { get; private set; }
        public int Function { get; private set; }
        public IEnumerable<Node> Parameters { get { return Children; } }
        public override string ToString() { return "KernelCall: " + Name; }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Function == ((KernelCall)n).Function;
        }
    }

    class PublicCall : Node
    {
        public PublicCall(string name, UInt16 script, UInt16 export, params Node[] children) :
            base(NodeType.PublicCall) { Name = name; Script = script; Export = export; Add(children); }
        public string Name { get; private set; }
        public UInt16 Script { get; private set; }
        public UInt16 Export { get; private set; }
        public IEnumerable<Node> Parameters { get { return Children; } }

        public override string ToString() { return "PublicCall: " + Name; }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Script == ((PublicCall)n).Script && Export == ((PublicCall)n).Export;
        }
    }

    class LocalCall : Node
    {
        public LocalCall(string name, int offset, params Node[] parameters) :
            base(NodeType.LocalCall, parameters) { Name = name; Offset = offset; }
        public string Name { get; private set; }
        public int Offset { get; private set; }
        public IEnumerable<Node> Parameters { get { return Children; } }
        public override string ToString() { return "LocalCall: " + Name; }

        public override void Accept(Visitor visitor) { AcceptChildren(visitor); visitor.Visit(this); }
        public override bool Same(Node n)
        {
            return base.Same(n) && Offset == ((LocalCall)n).Offset;
        }
    }

    //
    // AST Visitor; depth first. See `Accept` methods in each node.
    //

    abstract class Visitor
    {
        public virtual void Visit(Node node) { }
        public virtual void Visit(Number node) { }
        public virtual void Visit(String node) { }
        public virtual void Visit(Said node) { }
        public virtual void Visit(Selector node) { }
        public virtual void Visit(Class node) { }
        public virtual void Visit(Obj node) { }
        public virtual void Visit(Variable node) { }
        public virtual void Visit(ComplexVariable node) { }
        public virtual void Visit(Property node) { }
        public virtual void Visit(Assignment node) { }
        public virtual void Visit(Increment node) { }
        public virtual void Visit(Decrement node) { }
        public virtual void Visit(AddressOf node) { }
        public virtual void Visit(Compare node) { }
        public virtual void Visit(If node) { }
        public virtual void Visit(Switch node) { }
        public virtual void Visit(Cond node) { }
        public virtual void Visit(Case node) { }
        public virtual void Visit(Else node) { }
        public virtual void Visit(Loop node) { }
        public virtual void Visit(Send node) { }
        public virtual void Visit(SendMessage node) { }
        public virtual void Visit(KernelCall node) { }
        public virtual void Visit(PublicCall node) { }
        public virtual void Visit(LocalCall node) { }
    }
}
