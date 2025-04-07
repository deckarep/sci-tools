using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Vocab handles vocabulary resources used by the SCI "said" parser.
//
// This parser is for the word database in VOCAB.000 or VOCAB.900.
// Not to be confused with VOCAB.996 (classes) or VOCAB.997 (selectors).
//
// The database is just a list of words with classes and groups.
//
// In SCI0, it's VOCAB.000.
// In SCI1, it's VOCAB.900 in a newer format.
// In PQ2 Japan, it's VOCAB.000 in the newer format.
// In Hoyle1, it's VOCAB.000, and it's unused garbage.
// In dual language games, the second language is in a resource
// with a different number, but I don't care about those.

namespace SCI.Resource
{
    public enum VocabVersion
    {
        None,
        SCI0,
        SCI1
    }

    public class Vocab
    {
        public VocabVersion Version;
        public List<Word> Words;
        public Dictionary<UInt16, List<string>> WordsByGroup;

        // SCI Companion behavior
        public string GetPreferredWord(UInt16 group)
        {
            List<string> words;
            if (WordsByGroup.TryGetValue(group, out words))
            {
                foreach (var word in words)
                {
                    if (PreferredWords.Contains(word))
                    {
                        return word;
                    }
                }
                return words[0];
            }
            else
            {
                // LSL3 Demo script 395 has non-existent groups
                return "--invalid--";
            }
        }

        // Preferred words to use when rendering a Said string.
        //
        // Compiled scripts reference vocab words by group id. Unless there's
        // only one word in a group, we can't know which word a programmer typed
        // into the source code, so we have to pick one. If you don't do this
        // well, then your results will be confusing and appalling.
        //
        // The naive approach is to pick the first word (alphabetical) since
        // they're all supposed to be synonyms. But that will get you "gaze"
        // instead of "look", "acquire" instead of "get", etc. Basic swears need
        // to be preferred, otherwise you'll get "boff" instead of "fuck".
        // "man" needs to be a preferred word because developers would add every
        // male character's name as a synonym, and sometimes their own names.
        // "woman" absolutely needs to be a preferred word because otherwise
        // your output will feature the vocabulary of unsupervised 80s dudes.
        static string[] PreferredWords = {
            "look", "get", "talk", "climb", "listen",
            "eat", "wear", "fuck", "shit", "kill",
            "water", "yes", "no", "maybe", "me", "you",
            "start", "stop", "yell", "make", "cut", "give",
            "hide", "hit", "pull", "drop", "read", "open",
            "show", "stab", "throw", "woman", "man",
            "merlin", // as opposed to enchanter in camelot
        };

        // if the caller knows the version then use it, otherwise autodetect.
        // this is necessary because vocab.000 could be either.
        public static Vocab Read(Span span, VocabVersion version)
        {
            if (version == VocabVersion.None)
            {
                version = DetectVersion(span);
            }
            if (version == VocabVersion.None)
            {
                return null;
            }
            return ReadVocab(span, version);
        }

        public static Vocab ReadVocab(Span span, VocabVersion version)
        {
            var vocab = new Vocab { Version = version, Words = new List<Word>(), WordsByGroup = new Dictionary<ushort, List<string>>() };
            var stream = new SpanStream(span);

            // Indexes to the first word of each letter.
            // Used to speed-up lookup, not used by ScummVM or SCI Companion.
            // 26 initially, then more for localization.
            if (version == VocabVersion.SCI0)
            {
                stream.Skip(26 * 2);
            }
            else
            {
                stream.Skip(255 * 2);
            }

            // Each word is compressed text followed by class and group.
            // Text begins with a "copy count" byte that says how many characters
            // to copy from the previously parsed string. This compresses since
            // the strings are sorted alphabetically.
            // Text is terminated by the final character having its high bit set.
            int previousWordLength = 0;
            while (!stream.EOF)
            {
                // copy characters from previous word
                byte copyCount = stream.ReadByte();
                if (copyCount > previousWordLength)
                {
                    // bad data -- Jones vocab.900, it doesn't have a parser.
                    // this is how SCI Companion rejects it.
                    return null;
                }
                var text = new StringBuilder();
                if (copyCount != 0)
                {
                    text.Append(vocab.Words.Last().Text, 0, copyCount);
                }

                // read characters from current word.
                // Sci0 terminates by setting the high bit,
                // Sci1 terminates with a nul character.
                if (version == VocabVersion.SCI0)
                {
                    byte b;
                    while ((b = stream.ReadByte()) < 0x80)
                    {
                        text.Append((char)b);
                    }
                    text.Append((char)(b & 0x7f));
                }
                else
                {
                    byte b;
                    while ((b = stream.ReadByte()) != 0)
                    {
                        text.Append((char)b);
                    }
                }
                previousWordLength = text.Length;

                // Class and Group are each 12 bits of 3 bytes.
                byte cg0 = stream.ReadByte();
                byte cg1 = stream.ReadByte();
                byte cg2 = stream.ReadByte();
                var properties = new WordProperties
                {
                    Class = (UInt16)((cg0 << 4) | (cg1 >> 4)),
                    Group = (UInt16)(((cg1 & 0x0f) << 8) | cg2)
                };

                // words can have multiple entries in Sci1
                var wordText = text.ToString();
                var word = vocab.Words.LastOrDefault(w => w.Text == wordText);
                if (word != null)
                {
                    if (version == VocabVersion.SCI0)
                    {
                        throw new Exception("Multiple vocab entries for: " + word);
                    }
                    word.Properties.Add(properties);
                }
                else
                {
                    word = new Word
                    {
                        Text = wordText,
                        Properties = new List<WordProperties> { properties }
                    };
                    vocab.Words.Add(word);
                }

                // make an index for quick lookups
                if (!vocab.WordsByGroup.ContainsKey(properties.Group))
                {
                    vocab.WordsByGroup.Add(properties.Group, new List<string>());
                }
                // this dupe check may not be necessary
                if (!vocab.WordsByGroup[properties.Group].Contains(word.Text))
                {
                    vocab.WordsByGroup[properties.Group].Add(word.Text);
                }
            }

            return vocab;
        }

        // Games where parsing VOCAB.000 as Vocab0 will fail:
        // - Hoyle 1; VOCAB.000 is garbage. There is no parser.
        // - PQ2 Japan; it's the SCI1 format that's normally in VOCAB.900
        //
        // DetectVersion() handles both of these by validating the letter index
        // at the start of the resource. Each of the 26 offsets should point
        // to the first word of the letter in the resource. If any are
        // out of bounds, or if they are all zero, then this isn't a Vocab0.
        public static VocabVersion DetectVersion(Span span)
        {
            // i haven't seen any this short
            if (span.Length < 26 * 2) return VocabVersion.None;

            bool allZero = true;
            for (int i = 0; i < 26; i++)
            {
                UInt16 firstWordOffset = span.GetUInt16(i * 2);
                if (firstWordOffset != 0)
                {
                    allZero = false;
                }
                if (firstWordOffset >= span.Length)
                {
                    // hoyle1 or any other junk ones
                    return VocabVersion.None;
                }
            }

            // ScummVM handles PQ2 Japan by changing to SCI1 format if
            // the first 26 offsets are zero.
            return !allZero ? VocabVersion.SCI0 : VocabVersion.SCI1;
        }
    }

    public class Word
    {
        public string Text;
        public List<WordProperties> Properties;

        public override string ToString()
        {
            return Text + " " + string.Join(", ", Properties.Select(p => p.ToString()));
        }
    }

    public class WordProperties
    {
        public UInt16 Class;
        public UInt16 Group;

        public override string ToString()
        {
            return string.Format("[Class: {0:X4}, Group: {1:X4}]", Class, Group);
        }
    }
}
