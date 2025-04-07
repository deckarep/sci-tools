using System.Collections.Generic;
using System.Linq;

namespace SCI.Language
{
    public enum TokenType
    {
        LeftParen,
        RightParen,
        LeftBracket,
        RightBracket,
        AtSign,

        Integer,
        String,
        Said,
        Atom,

        EOF
    }

    public class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public IReadOnlyList<Trivia> LeftTrivia { get; private set; }
        public IReadOnlyList<Trivia> RightTrivia { get; private set; }

        public int Pos { get; private set; }
        public int Line { get; private set; }
        public int Col { get; private set; }

        public Token(TokenType type, string value,
                     IEnumerable<Trivia> leftTrivia, IEnumerable<Trivia> rightTrivia,
                     int pos, int line, int col)
        {
            Type = type;
            Value = value;
            LeftTrivia = leftTrivia.ToList();
            RightTrivia = rightTrivia.ToList();
            Pos = pos;
            Line = line;
            Col = col;
        }

        public override string ToString()
        {
            return Type + ": " + Value;
        }
    }

    public enum TriviaType
    {
        Whitespace,
        NewLine,
        Comment,
    }

    public class Trivia
    {
        public TriviaType Type { get; private set; }
        public string Text { get; private set; }

        public Trivia (TriviaType type, string text )
        {
            Type = type;
            Text = text;
        }

        public override string ToString()
        {
            return Type + ": " + ((Type == TriviaType.Comment) ? Text : (Text.Length + " characters"));
        }
    }
}
