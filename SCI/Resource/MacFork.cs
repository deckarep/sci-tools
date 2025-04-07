using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// MacFork.cs handles generic Mac resource fork parsing.
// It's like scummvm's macresman.cpp.
//
// Handles:
// *.rsrc
// *.bin [ MacBinary ]
// *     [ MacBinary ]
//
// Usage:
// var resourceFork = MacResourceFork.Read("Data1")
//     foreach var type in resourceFork.Types
//         foreach var entry in type.Entries
//             do something!

namespace SCI.Resource
{
    public enum MacResourceFormat
    {
        None,
        Raw,
        MacBinary,
    }

    public class MacResourceFork
    {
        public string Name;
        public MacResourceFormat Format;
        public List<MacResourceType> Types;
        public Span Span;

        // header
        public UInt32 DataOffset;
        public UInt32 MapOffset;
        public UInt32 DataLength;
        public UInt32 MapLength;

        // map
        public UInt32 MapResAttr;
        public UInt16 MapTypeOffset;
        public UInt16 MapNameOffset;
        public UInt16 MapNumTypes;

        public override string ToString()
        {
            return Name + ", " + Types.Count + " types";
        }

        // usage: Read("Data1")
        public static MacResourceFork Read(string fileName)
        {
            // 1. Raw with .rsrc extension
            // 2. MacBinary with .bin extension
            // 3. MacBinary with no extension
            string rsrcFileName = fileName + ".rsrc";
            var format = MacResourceFormat.None;
            Span forkSpan = null;
            if (File.Exists(rsrcFileName))
            {
                format = MacResourceFormat.Raw;
                forkSpan = new Span(rsrcFileName, Endian.Big);
            }
            if (forkSpan == null)
            {
                rsrcFileName = fileName + ".bin";
                if (File.Exists(rsrcFileName))
                {
                    format = MacResourceFormat.MacBinary;
                    forkSpan = MacBinary.Read(new Span(rsrcFileName, Endian.Big));
                }
            }
            if (forkSpan == null)
            {
                format = MacResourceFormat.MacBinary;
                forkSpan = MacBinary.Read(new Span(fileName, Endian.Big));
            }

            var fork = Read(forkSpan);
            fork.Name = Path.GetFileName(fileName);
            fork.Format = format;
            return fork;
        }

        public static MacResourceFork Read(Span span)
        {
            var fork = new MacResourceFork() { Span = span };
            var stream = new SpanStream(span);

            // header
            fork.DataOffset = stream.ReadUInt32BE();
            fork.MapOffset = stream.ReadUInt32BE();
            fork.DataLength = stream.ReadUInt32BE();
            fork.MapLength = stream.ReadUInt32BE();

            // map
            stream.Seek(fork.MapOffset + 22); // why 22?
            fork.MapResAttr = stream.ReadUInt16BE();
            fork.MapTypeOffset = stream.ReadUInt16BE();
            fork.MapNameOffset = stream.ReadUInt16BE();
            fork.MapNumTypes = stream.ReadUInt16BE();
            fork.MapNumTypes++;

            // resource types
            fork.Types = new List<MacResourceType>(fork.MapNumTypes);
            stream.Seek(fork.MapOffset + fork.MapTypeOffset + 2); // why 2?
            for (int i = 0; i < fork.MapNumTypes; i++)
            {
                var type = new MacResourceType();
                fork.Types.Add(type);
                type.Id = stream.ReadUInt32BE();
                type.Name = MacResourceFork.TypeToString(type.Id);
                type.EntryCount = stream.ReadUInt16BE();
                type.EntryCount++;
                type.Offset = stream.ReadUInt16BE();
                type.Entries = new List<MacResourceEntry>(type.EntryCount);
            }

            // resource entries, one type at a time
            foreach (var type in fork.Types)
            {
                // read each entry for the type, except name and data
                stream.Seek(type.Offset + fork.MapOffset + fork.MapTypeOffset);
                for (int i = 0; i < type.EntryCount; i++)
                {
                    var entry = new MacResourceEntry();
                    type.Entries.Add(entry);
                    entry.Id = stream.ReadUInt16BE();
                    entry.NameOffset = stream.ReadUInt16BE();
                    // attributes:    8 bits
                    // data offset:  24 bits
                    // would be nice to add 24-bit span/stream methods
                    entry.DataOffset = stream.ReadUInt32BE();
                    entry.Attributes = (byte)(entry.DataOffset >> 24);
                    entry.DataOffset &= 0x00ffffff;
                    stream.Skip(4);
                }

                // read each entry's name. names are optional.
                foreach (var entry in type.Entries)
                {
                    if (entry.NameOffset == 0xffff)
                    {
                        entry.Name = "";
                        continue;
                    }
                    stream.Seek(entry.NameOffset + fork.MapOffset + fork.MapNameOffset);
                    byte nameLength = stream.ReadByte();
                    entry.Name = stream.ReadString(nameLength);
                }

                // make a span for each entry's data
                foreach (var entry in type.Entries)
                {
                    stream.Seek(fork.DataOffset + entry.DataOffset);
                    UInt32 dataLength = stream.ReadUInt32BE();
                    entry.Span = stream.ReadBytes((int)dataLength);
                }
            }

            return fork;
        }

        public static UInt32 Type(string s)
        {
            if (s.Length != 4) throw new Exception("Invalid Mac resource type: " + s);
            UInt32 t = ((UInt32)s[0] << 24) |
                       ((UInt32)s[1] << 16) |
                       ((UInt32)s[2] << 8) |
                       ((UInt32)s[3] << 0);
            return t;
        }

        public static string TypeToString(UInt32 t)
        {
            var sb = new StringBuilder(4);
            sb.Append((char)(t >> 24));
            sb.Append((char)(t >> 16 & 0x000000ff));
            sb.Append((char)(t >> 8  &  0x000000ff));
            sb.Append((char)(t >> 0  &  0x000000ff));
            return sb.ToString();
        }
    }

    public class MacResourceType
    {
        public UInt32 Id;
        public string Name; // printable version of Id
        public UInt16 EntryCount;
        public UInt16 Offset;

        public List<MacResourceEntry> Entries;

        public override string ToString()
        {
            return Name + ", " + Entries.Count + " entries";
        }
    }

    public class MacResourceEntry
    {
        public UInt16 Id;
        public UInt16 NameOffset;
        public UInt32 DataOffset;
        public byte Attributes;

        public string Name;
        public Span Span;

        public override string ToString()
        {
            if (Name == "")
            {
                return string.Format("{0}, {1} bytes", Id, Span.Length);
            }
            else
            {
                return string.Format("{0} \"{1}\", {2} bytes", Id, Name, Span.Length);
            }
        }
    }
}
