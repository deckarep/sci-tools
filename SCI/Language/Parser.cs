using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;

// SCI Script Parser (Source code, not compiled scripts)
//
// Creates a parse tree containing everything in a script file
// so that it can be queried, changed, and written back.
//
// Originally written for SCI Companion's decompiler output.
// Much later I wrote my decompiler, but its output conforms to
// Companion's conventions so this parser didn't change.
// This parser was used to validate my decompiler's output.
//
// Works on Sierra's original source too, but there may be some
// syntax I don't know about that breaks it. Parser just builds
// a parse tree; the Model classes interpret it and handle things
// like changes in keywords.

namespace SCI.Language
{
    public class Parser
    {
        Lexer lexer;

        public Parser(string text)
        {
            lexer = new Lexer(text);
        }

        public Root Parse()
        {
            var root = new Root();
            Collection currentCollection = root;
            var stack = new Stack<Collection>();
            stack.Push(currentCollection);

            Token token;
            do
            {
                Node node = null;

                token = lexer.GetNextToken();
                switch (token.Type)
                {
                    case TokenType.LeftParen:
                    case TokenType.LeftBracket:
                    case TokenType.AtSign:
                        Collection collection;
                        if (token.Type == TokenType.LeftParen)
                        {
                            collection = new List(currentCollection);
                        }
                        else if (token.Type == TokenType.LeftBracket)
                        {
                            collection = new Array(currentCollection);
                        }
                        else
                        {
                            collection = new AddressOf(currentCollection);
                        }
                        currentCollection.AddChild(collection);
                        stack.Push(currentCollection);
                        currentCollection = collection;
                        node = collection;
                        break;

                    case TokenType.RightParen:
                    case TokenType.RightBracket:
                        currentCollection.EndLeftTrivia = token.LeftTrivia.ToList();
                        currentCollection.EndRightTrivia = token.RightTrivia.ToList();
                        currentCollection = stack.Pop();
                        break;

                    case TokenType.Atom:
                        node = new Atom(currentCollection, token.Value);
                        currentCollection.AddChild(node);
                        break;

                    case TokenType.Integer:
                        node = new Integer(currentCollection, token.Value, ParseInteger(token.Value));
                        currentCollection.AddChild(node);
                        break;

                    case TokenType.String:
                        node = new String(currentCollection, token.Value, ParseString(token.Value));
                        currentCollection.AddChild(node);
                        break;

                    case TokenType.Said:
                        node = new Said(currentCollection, token.Value, ParseString(token.Value));
                        currentCollection.AddChild(node);
                        break;

                    case TokenType.EOF:
                        break;

                    default:
                        throw new Exception("Unrecognized token: " + token);
                }

                if (node != null)
                {
                    node.Pos = token.Pos;
                    node.Line = token.Line;
                    node.Col = token.Col;
                    node.LeftTrivia = token.LeftTrivia.ToList();
                    node.RightTrivia = token.RightTrivia.ToList();
                }

                // AddressOf is a collection with only one child and it doesn't have
                // a token to indicate that it's terminated, so check after each node
                if (currentCollection is AddressOf && currentCollection.Children.Any())
                {
                    currentCollection = stack.Pop();
                }

            } while (token.Type != TokenType.EOF);

            stack.Pop();
            if (stack.Count != 0) throw new Exception("Incomplete text, stack not empty");
            return root;
        }

        public static string ParseString(string text)
        {
            // string literals can be surrounded by { } or " " or ' '.
            // when initializing a property, quotes are used.
            // kSaid strings use single quotes
            return text.Substring(1, text.Length - 2);
        }

        public static int ParseInteger(string text)
        {
            if (text.StartsWith("$"))
            {
                UInt16 number;
                if (!UInt16.TryParse(text.Substring(1), NumberStyles.HexNumber, null, out number))
                {
                    throw new Exception("Unable to parse hex integer: " + text);
                }
                return number;
            }
            else
            {
                Int16 signed;
                if (Int16.TryParse(text, out signed))
                {
                    return signed;
                }
                UInt16 unsigned;
                if (UInt16.TryParse(text, out unsigned))
                {
                    return unsigned;
                }
                throw new Exception("Unable to parse decimal integer: " + text);
            }
        }
    }
}
