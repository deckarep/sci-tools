using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// The SCI script tree produced by Parser.
//
// This tree captures 100% of what's in a script file including whitespace,
// newlines, and comments so that TreeWriter can produce the identical script
// file from the parsed tree.
//
// The tree does not support modifying the structure. Nodes can't be added or
// removed and I used IReadOnlyList to enforce that. I'm currently only
// interested in changing the text of existing nodes and adding comments.
//
// Whitespace, newlines, and comments are called "trivia" and they hang off of
// the left and right side of each node, though they're usually only present on
// one side since the adjacent node gets parsed with the other side.
// This is entirely lifted from a stackoverflow answer by Eric Lippert where he
// described writing something like this at microsoft and called it a
// "full fidelity" parser/lexer. Thank you, Eric!
//
// Annotators add comments to the tree by annotating nodes. Existing comments
// are parsed in as trivia but new comments are added as annotations by calling
// Node.Annotate(). Nodes can accumulate multiple annotations. TreeWriter then
// writes these annotations to file in order. If two nodes on the same line have
// annotations then a comment will be added with the first node's annotations,
// in the order they were added, and then the second node's annotations, in the
// order they were added, with each annotation separated by a comma and a space.
// Any existing comment is overridden when there is an annotation on that line.
//
// Node
//   Atom
//   Integer
//   String
//   Said
//   Nil
//   Collection
//     List
//       Root
//     Array

namespace SCI.Language
{
    public abstract class Node : IEnumerable<Node>
    {
        protected static Nil nil = new Nil();
        public static Nil Nil { get { return nil; } }
        static IReadOnlyList<Node> emptyList = new List<Node>(0);

        public Node Parent { get; protected set; }

        public abstract string Text { get; }  // original source text. empty for lists/arrays
        public abstract object Value { get; } // parsed value. same as original source text for atoms.
        public virtual int Number { get { throw new Exception("Not an Integer: " + this); } }

        public virtual IReadOnlyList<Node> Children { get { return emptyList; } }

        public int Pos { get; set; }
        public int Line { get; set; }
        public int Col { get; set; }

        public List<Trivia> LeftTrivia { get; set; }
        public List<Trivia> RightTrivia { get; set; }

        public List<string> Annotations { get; private set; } // null unless one is added

        // returns a child node by index. if the index is invalid then Nil is returned.
        public Node At(int index)
        {
            return (0 <= index && index < Children.Count) ? Children[index] : nil;
        }

        // returns a node's sibling by offset, which can be negative. returns Nil if no node.
        // loops through the parents children until the current node is found.
        public Node Next(int offset = 1)
        {
            // handle both Root and Nil here
            if (Parent == null)
            {
                return nil;
            }

            int index = Parent.Children.IndexOf(this);
            int siblingIndex = index + offset;
            return Parent.At(siblingIndex);
        }

        // returns the first node whose Text matches
        public Node FindChild(string text)
        {
            return Children.FirstOrDefault(n => n.Text == text) ?? nil;
        }

        // returns the first node whose first child's Text matches
        public Node FindChildList(string text)
        {
            return Children.FirstOrDefault(n => n.At(0).Text == text);
        }

        // the magic behind this all of this. enumerating a node visits the
        // node itself and all of its children recursively. this is depth-first
        // so the first child is visited, then that child's first child is visited...
        // i never understood the yield statement until now.
        public IEnumerator<Node> GetEnumerator()
        {
            var stack = new Stack<Node>();
            stack.Push(this);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                foreach (var child in node.Children.Reverse())
                {
                    stack.Push(child);
                }
                yield return node; // the first yield i've ever written
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Annotate(string annotation)
        {
            if (Annotations == null)
            {
                Annotations = new List<string>();
            }
            else if (Annotations.Contains(annotation))
            {
                return;
            }
            Annotations.Add(annotation);
        }
    }

    // Nil never appears in any trees, has no children, and is its own parent.
    // Nil is the null-value node. This is a null-free zone!
    public class Nil : Node
    {
        public override string Text { get { return ""; } }
        public override object Value { get { return ""; } }

        public Nil()
        {
            Parent = this;
        }

        public override string ToString()
        {
            return "NIL";
        }

        public override void Annotate(string annotation)
        {
            throw new Exception("Annotating Nil");
        }
    }

    // an Atom is an operator, keyword, symbol, pretty much everything except for
    // a number or a string or a said, and a said is just a special string.
    public class Atom : Node
    {
        string text;

        public Atom(Node parent, string text)
        {
            Parent = parent;
            this.text = text;
        }

        public override string Text { get { return text; } }
        public override object Value { get { return text; } }

        // i haven't thought this through, i just want to get symbol renaming working
        public void SetText(string text)
        {
            this.text = text;
        }

        public override string ToString()
        {
            return "ATOM: " + Text;
        }
    }

    // Strings are string literals which depending on their context are surrounded
    // by curly brackets or double quotes. they also have escaping rules. once
    // parsed, i'm just tracking both the original escaped text and the text
    // as i've unescaped it so that the original string is written back and so i
    // can query/print the escaped string if i need to. this would be more complicated
    // if i supported updating String text in the tree but i don't need that yet.
    // i will need to do that eventually if i write a normalizer to strip foreign
    // languages from multi-language strings for diffing. that's a special case,
    // maybe i'll just add a dedicated method for that to this class.
    public class String : Node
    {
        string text;  // escaped
        string value; // unescaped

        public String(Node parent, string text, string value)
        {
            Parent = parent;
            this.text = text;
            this.value = value;
        }

        public override string Text { get { return text; } }
        public override object Value { get { return value; } }

        // not thought out!! this is escaped text, i'm only using this
        // now to strip out foreign language strings, leaving only english
        public void SetText(string text)
        {
            this.text = text;
        }

        public override string ToString()
        {
            return "STRING: " + Value;
        }
    }

    // a Said is effectively the same as a String but it's surrounded by
    // single quotes so i need to track the difference. they really are
    // different though and you'd never query for one type and want the other.
    public class Said : Node
    {
        string text;  // escaped
        string value; // unescaped

        public Said(Node parent, string text, string value)
        {
            Parent = parent;
            this.text = text;
            this.value = value;
        }

        public override string Text { get { return text; } }
        public override object Value { get { return value; } }

        public override string ToString()
        {
            return "SAID: " + Value;
        }
    }

    public class Integer : Node
    {
        string text;
        int value;

        public Integer(Node parent, string text, int value)
        {
            Parent = parent;
            this.text = text;
            this.value = value;
        }

        public override string Text { get { return text; } }
        public override object Value { get { return value; } }
        public override int Number { get { return value; } }

        public void SetHexFormat()
        {
            MakeUnsigned();
            this.text = "$" + ((UInt16)value).ToString("x4");
        }

        public void MakeUnsigned()
        {
            if (value < 0)
            {
                // "flip the bits, add one"
                value = (-value ^ 0xffff) + 1;

                // update text based on format
                if (text.StartsWith("$"))
                {
                    SetHexFormat();
                }
                else
                {
                    text = value.ToString();
                }
            }
        }

        public void MakeSigned()
        {
            if (value >= 0x8000)
            {
                // "flip the bits, add one"
                value = (-value ^ 0xffff) + 1;

                // update text
                text = value.ToString();
            }
        }

        // i *really* haven't thought this through! this is for replacing an integer
        // with a definition symbol from sci.h. it's a hack to work around my
        // self imposed limitation that the node structure can't be modified.
        // this means that the next time this is parsed it won't be an Integer node.
        public void SetDefineText(string text)
        {
            this.text = text;
        }

        public override string ToString()
        {
            return "INTEGER: " + Value;
        }
    }

    // Collection is an abstract public class so that i could factor out the common stuff
    // that List and Array both need. AddressOf (@ sign) is also a Collection but
    // with only one child.
    public abstract class Collection : Node
    {
        protected List<Node> nodes;

        public override string Text { get { return ""; } }
        public override object Value { get { return ""; } }

        public override IReadOnlyList<Node> Children { get { return nodes; } }

        // every Node except a Collection represents a single token which
        // trivia hangs off of. Collections have start and end tokens
        // so they have a second set of trivia for the end paren/bracket.
        public List<Trivia> EndLeftTrivia { get; set; }
        public List<Trivia> EndRightTrivia { get; set; }

        public virtual void AddChild(Node node)
        {
            nodes.Add(node);
        }
    }

    // a List is surrounded by parenthesis
    public class List : Collection
    {
        public List(Node parent)
        {
            Parent = parent;
            nodes = new List<Node>();
        }

        public override string ToString()
        {
            return "LIST: " + Children.Count;
        }
    }

    // Root is a List that has no parent
    public class Root : List
    {
        public Root() : base(nil)
        {
        }

        public override string ToString()
        {
            return "ROOT: " + Children.Count;
        }
    }

    // an Array is surrounded by brackets
    public class Array : Collection
    {
        public Array(Node parent)
        {
            Parent = parent;
            nodes = new List<Node>();
        }

        public override string ToString()
        {
            return "ARRAY: " + Children.Count;
        }
    }

    // AddressOf is a Collection where only one child is allowed.
    // usage: @variable
    //        @(= variable ...)
    public class AddressOf : Collection
    {
        public AddressOf(Node parent)
        {
            Parent = parent;
            nodes = new List<Node>();
        }

        public override string Text { get { return "@"; } }
        public override object Value { get { return "@"; } }

        public override void AddChild(Node node)
        {
            if (nodes.Any())
            {
                throw new Exception("Attempting to add multiple children to AddressOf: " + node);
            }
            base.AddChild(node);
        }

        public override string ToString()
        {
            return "@" + nodes.FirstOrDefault();
        }
    }

    // some IReadOnlyList/IEnumerable extensions. i will probably relent on using
    // IReadOnlyList if i ever make something that alters the tree structure, but
    // so far it's just changing existing node text and adding comments.
    public static class Extensions
    {
        // IReadOnlyList doesn't have IndexOf or LastIndexOf
        public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list), $"{nameof(list)} is null ");

            for (var i = 0; i < list.Count; i++)
            {
                var value = list[i];
                if (value == null)
                {
                    if (item == null)
                    {
                        return i;
                    }
                }
                else if (value.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int IndexWhere<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            int index = 0;
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
