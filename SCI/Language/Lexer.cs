using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

// SCI Script Lexer

namespace SCI.Language
{
    public class Lexer
    {
        string text;
        char currentChar;
        int pos;
        int line;
        int col;

        public Lexer(string text)
        {
            this.text = text;
            currentChar = (text.Length == 0) ? '\0' : text[pos];
        }

        public Token GetNextToken()
        {
            var leftTrivia = Trivias();

            int currentPos = pos;
            int currentLine = line;
            int currentCol = col;

            if (currentChar == '\0')
            {
                return new Token(TokenType.EOF, "", leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == '(')
            {
                Advance();
                return new Token(TokenType.LeftParen, "(", leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == ')')
            {
                Advance();
                return new Token(TokenType.RightParen, ")", leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == '[')
            {
                Advance();
                return new Token(TokenType.LeftBracket, "[", leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == ']')
            {
                Advance();
                return new Token(TokenType.RightBracket, "]", leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == '@')
            {
                Advance();
                return new Token(TokenType.AtSign, "@", leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == '{' || currentChar == '\"')
            {
                return new Token(TokenType.String, String(), leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == '\'')
            {
                return new Token(TokenType.Said, String(), leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == '$')
            {
                return new Token(TokenType.Integer, HexInteger(), leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (IsDecimalDigit(currentChar))
            {
                return new Token(TokenType.Integer, DecimalInteger(), leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == '-' && IsDecimalDigit(PeekChar()))
            {
                return new Token(TokenType.Integer, DecimalInteger(), leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }
            if (currentChar == ',')
            {
                // commas only appear in asm call instructions immediately after a procedure name.
                // this also requires comma to be a delimiter when reading atoms, which does most
                // of the work. this handling here isn't necessary with default decompiler output.
                Advance();
                return new Token(TokenType.Atom, ",", leftTrivia, Trivias(), currentPos, currentLine, currentCol);
            }

            // accept everything else as an atom
            return new Token(TokenType.Atom, Atom(), leftTrivia, Trivias(), currentPos, currentLine, currentCol);
        }

        void Advance()
        {
            pos += 1;
            if (pos < text.Length)
            {
                if (currentChar == '\n')
                {
                    line += 1;
                    col = 0;
                }
                else
                {
                    col += 1;
                }
                currentChar = text[pos];
            }
            else
            {
                col += 1;
                currentChar = '\0';
            }
        }

        char PeekChar()
        {
            int peekPos = pos + 1;
            return (peekPos < text.Length) ? text[peekPos] : '\0';
        }

        bool PeekString(string peekText)
        {
            for (int i = 0; i < peekText.Length; ++i)
            {
                int peekPos = pos + i;
                if (!(peekPos < text.Length && text[peekPos] == peekText[i]))
                {
                    return false;
                }
                if (i == peekText.Length - 1)
                {
                    return true;
                }
            }
            return false;
        }

        string String()
        {
            // string literals can be surrounded by { } or " " or ' '.
            // when initializing a property, quotes are used.
            // kSaid strings use single quotes
            var result = new StringBuilder();
            char endChar = (currentChar == '{') ? '}' : currentChar;
            result.Append(currentChar);
            Advance();
            bool escaped = false;
            while (currentChar != '\0')
            {
                if (escaped)
                {
                    escaped = false;
                }
                else if (currentChar == '\\')
                {
                    escaped = true;
                }
                else if (currentChar == endChar)
                {
                    result.Append(currentChar);
                    Advance();
                    break;
                }

                result.Append(currentChar);
                Advance();
            }
            return result.ToString();
        }

        string Atom()
        {
            // read until a delimiter
            var result = new StringBuilder();
            while (currentChar != '\0' &&
                   currentChar != '(' &&
                   currentChar != ')' &&
                   currentChar != '[' &&
                   currentChar != ']' &&
                   currentChar != ';' &&
                   currentChar != ',' && // comma from asm call instructions
                   !IsNewLine(currentChar))
            {
                // HACK: SCI Companion names stuff "BAD SELECTOR" with a space.
                // in some contexts it uses an underscore, but not in others.
                // detect this and use an underscore. [ phant2 Str class ]
                if (IsWhiteSpace(currentChar))
                {
                    if (result.Length >= 3 &&
                        result[result.Length - 3] == 'B' &&
                        result[result.Length - 2] == 'A' &&
                        result[result.Length - 1] == 'D' &&
                        PeekString(" SELECTOR"))
                    {
                        currentChar = '_';
                    }
                    else
                    {
                        break;
                    }
                }

                result.Append(currentChar);
                Advance();
            }
            return result.ToString();
        }

        string DecimalInteger()
        {
            // could start with a negative
            string result = Atom();
            UInt16 number1;
            Int16 number2;
            if (!UInt16.TryParse(result, out number1) &&
                !Int16.TryParse(result, out number2))
            {
                throw new Exception("Unable to parse integer: " + result);
            }
            return result;
        }

        string HexInteger()
        {
            // always starts with a dollar sign
            var result = Atom();
            string hex = result.Substring(1);
            UInt16 number;
            if (!UInt16.TryParse(hex, NumberStyles.HexNumber, null, out number))
            {
                throw new Exception("Unable to parse hex integer: " + result);
            }
            return result;
        }

        IEnumerable<Trivia> Trivias()
        {
            var trivias = new List<Trivia>();

            while (currentChar != '\0')
            {
                if (currentChar == ';')
                {
                    trivias.Add(new Trivia(TriviaType.Comment, Comment()));
                }
                else if (IsNewLine(currentChar))
                {
                    trivias.Add(new Trivia(TriviaType.NewLine, NewLine()));
                }
                else if (IsWhiteSpace(currentChar))
                {
                    trivias.Add(new Trivia(TriviaType.Whitespace, WhiteSpace()));
                }
                else
                {
                    break;
                }
            }

            return trivias;
        }

        string Comment()
        {
            var result = new StringBuilder();
            while (currentChar != '\0' && !IsNewLine(currentChar))
            {
                result.Append(currentChar);
                Advance();
            }
            return result.ToString();
        }

        string NewLine()
        {
            var result = new StringBuilder();
            while (currentChar != '\0' && IsNewLine(currentChar))
            {
                result.Append(currentChar);
                Advance();
            }
            return result.ToString();
        }

        string WhiteSpace()
        {
            var result = new StringBuilder();
            while (currentChar != '\0' && IsWhiteSpace(currentChar))
            {
                result.Append(currentChar);
                Advance();
            }
            return result.ToString();
        }

        bool IsDecimalDigit(char c)
        {
            return '0' <= c && c <= '9';
        }

        bool IsNewLine(char c)
        {
            return c == '\r' || c == '\n';
        }

        bool IsWhiteSpace(char c)
        {
            return char.IsWhiteSpace(c) && !IsNewLine(c);
        }
    }
}
