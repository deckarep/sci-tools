using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Selector Table: VOCAB.997
//
// 0    word            count
// 2    word*count      offsets to strings (starting with length)
// ?    word+string     length+string (sometimes null terminated)
//
// I saw the bytes 0c 00 in between offsets and strings in a few vocabs.

namespace SCI.Resource
{
    public static class SelectorVocab
    {
        public static string[] Read(Span vocab)
        {
            if (!DetectEndianness(vocab))
            {
                // should never happen; it's a good heuristic
                throw new Exception("selector vocab endianness cannot be detected");
            }

            // selector count
            UInt16 count = vocab.GetUInt16(0);
            count++; // scummvm says they're off by one

            // selectors: a list of offsets that point to strings.
            // selector 0 is first, then selector 1...
            // each offset points to a string which starts with a word length
            // and may or may not be null terminated. unused selectors point
            // to the string "BAD SELECTOR", which is usually the first string.
            // if two selectors have the same name then they may have the same
            // offset and point to the same string.

            var selectors = new string[count];
            for (int i = 0; i < count; ++i)
            {
                // location of selector string (starting with length)
                UInt16 offset = vocab.GetUInt16(2 + (i * 2));

                // string length
                UInt16 length = vocab.GetUInt16(offset);

                // string follows length
                selectors[i] = vocab.GetString(offset + 2, length);
            }

            return selectors;
        }

        public static byte[] Generate(string[] selectors)
        {
            var selectorDictionary = new Dictionary<int, string>();
            foreach (var selector in selectors)
            {
                selectorDictionary.Add(selectorDictionary.Count, selector);
            }
            return Generate(selectorDictionary);
        }

        public static byte[] Generate(Dictionary<int, string> selectors)
        {
            var stream = new MemoryStream();
            var vocab = new BinaryWriter(stream);

            // build an array of names for every selector index, including missing ones,
            // and build a dictionary mapping name strings to their offsets in the file
            int selectorEntryCount = selectors.Keys.Max() + 1;
            var names = new string[selectorEntryCount];
            var nameOffsets = new Dictionary<string, int>();
            int currentNameOffset = 2 + (selectorEntryCount * 2);
            // sierra always put BAD SELECTOR first so whatever, lets do that too
            nameOffsets.Add("BAD SELECTOR", currentNameOffset);
            currentNameOffset += (2 + "BAD SELECTOR".Length); // advance
            for (int i = 0; i < selectorEntryCount; ++i)
            {
                // get name of this selector
                string name;
                if (!selectors.TryGetValue(i, out name))
                {
                    name = "BAD SELECTOR";
                }

                // record the name in the table
                names[i] = name;

                // get the offset for this name.
                // if we've already seen it then re-use the offset,
                // otherwise add it to the offset table.
                int nameOffset;
                if (!nameOffsets.TryGetValue(name, out nameOffset))
                {
                    nameOffsets.Add(name, currentNameOffset);
                    currentNameOffset += (2 + name.Length); // advance
                }
            }

            // write it

            // scummvm says they're off by one
            vocab.Write((UInt16)(selectorEntryCount - 1));
            // name offsets
            foreach (var name in names)
            {
                vocab.Write((UInt16)nameOffsets[name]);
            }
            // names
            foreach (var name in nameOffsets.OrderBy(kv => kv.Value).Select(kv => kv.Key))
            {
                vocab.Write((UInt16)name.Length);
                foreach (var c in name)
                {
                    vocab.Write((byte)c);
                }
            }

            var vocabBytes = stream.ToArray();
            vocab.Dispose();
            stream.Dispose();
            return vocabBytes;
        }

        public static bool DetectEndianness(Span vocab)
        {
            var originalEndian = vocab.Endian;

            vocab.Endian = Endian.Little;
            int leCount = DetectValidSelectors(vocab);
            vocab.Endian = Endian.Big;
            int beCount = DetectValidSelectors(vocab);

            // would never happen
            if (leCount == beCount)
            {
                vocab.Endian = originalEndian;
                return false;
            }

            vocab.Endian = (leCount > beCount) ? Endian.Little : Endian.Big;
            return true;
        }

        static int DetectValidSelectors(Span vocab)
        {
            UInt16 count = vocab.GetUInt16(0);
            count++;

            int endOfOffsetsPosition = count * 2 + 2;

            // nope, too big
            if (endOfOffsetsPosition >= vocab.Length) return 0;

            for (int i = 0; i < count; ++i)
            {
                UInt16 offset = vocab.GetUInt16(2 + (i * 2));

                if (!(endOfOffsetsPosition <= offset && offset <= vocab.Length - 2))
                {
                    // offset is out of bounds, here's how many we got
                    return i;
                }
            }

            // all selectors are reachable
            return count;
        }

        // SCI0 early scripts shift the selector left and use the
        // lower bit to indicate read vs write. Do what scummvm does
        // (and what i patched sci companion to do) and just create
        // two entries for each selector so that both lookups work.
        public static string[] CreateSci0EarlyTable(string[] selectors)
        {
            var sci0EarlySelectors = new string[selectors.Length * 2];
            for (int i = 0; i < selectors.Length; ++i)
            {
                sci0EarlySelectors[i * 2] = selectors[i];
                sci0EarlySelectors[i * 2 + 1] = selectors[i];
            }
            return sci0EarlySelectors;
        }
    }
}
