namespace SCI
{
    // Represents a message in a message resource.
    //
    // There were many versions of this format; it got more complex.
    // If a message references another message, then the interpreter
    // follows the reference recursively until it finds a real one
    // and uses that text.

    public class Message
    {
        public int ModNum; // uint16
        public int Noun; // byte
        public int Verb; // byte
        public int Cond; // byte
        public int Seq;  // byte
        public int Talker;
        public string Text;
        public int RefNoun; // byte
        public int RefVerb; // byte
        public int RefCond; // byte

        public override string ToString()
        {
            return ModNum + ": " + Noun + " " + Verb + " " + Cond + " " + Seq + ": " +
                   (IsRef ? ("REF " + RefNoun + " " + RefVerb + " " + RefCond) : Text);
        }

        // only in sci32 games
        public bool IsRef
        {
            get
            {
                return RefNoun != 0 || RefVerb != 0 || RefCond != 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Message m = (Message)obj;
            return ModNum.Equals(m.ModNum) &&
                   Noun.Equals(m.Noun) &&
                   Verb.Equals(m.Verb) &&
                   Cond.Equals(m.Cond) &&
                   Seq.Equals(m.Seq);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 19 + ModNum.GetHashCode();
                hash = hash * 19 + Noun.GetHashCode();
                hash = hash * 19 + Verb.GetHashCode();
                hash = hash * 19 + Cond.GetHashCode();
                hash = hash * 19 + Seq.GetHashCode();
                return hash;
            }
        }
    }
}
