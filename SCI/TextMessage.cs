namespace SCI
{
    // Represents a message in a Text resource

    public class TextMessage
    {
        public int ModNum { get; private set; }
        public int Number { get; private set; }
        public string Text { get; private set; }

        public TextMessage(int modNum, int number, string text)
        {
            ModNum = modNum;
            Number = number;
            Text = text;
        }

        // this exists for stripping foreign languages
        public void SetText(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return ModNum + ": " + Number + " " + Text;
        }
    }
}
