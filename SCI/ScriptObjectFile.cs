using System;
using System.Collections.Generic;
using System.IO;
using SCI.Resource;

// SCI Companion creates a binary .sco file for each decompiled script.
// These files need to exist and be kept up to date for the IDE and
// syntax highlighting to recognize calls to exported functions, etc.
//
// My decompiler needs to generate these, and annotators needs to keep them
// up to date with renames. The file contains selectors and class numbers,
// which annotators won't know, but that's fine because they don't change.
// Annotators just need to keep export names, class names, and local names
// up to date. And I don't think locals really matter, but globals might.
//
// If Companion could regenerate .sco files from source then I wouldn't need
// to do this and include thousands of them in my sci-scripts repo.

namespace SCI
{
    public class ScriptObjectFile
    {
        public bool SeparateHeap;
        public UInt16 Number;
        public List<ExportInfo> Exports;
        public List<ClassInfo> Classes;
        public List<string> Locals;

        public class ExportInfo
        {
            public UInt16 Number;
            public string Name;

            public override string ToString() { return Number.ToString() + " - " + Name; }
        }

        public class ClassInfo
        {
            public string Name;
            public UInt16 Species;
            public UInt16 Super;
            public bool HasNameProperty; // when false, prop count == -1
            public List<PropertyInfo> Properties;
            public List<UInt16> Methods; // selectors

            public override string ToString() { return Name; }
        }

        public class PropertyInfo
        {
            public UInt16 Selector;
            public UInt16 Value;

            public override string ToString() { return Selector.ToString("x4") + " - " + Value.ToString("x4"); }
        }

        public static ScriptObjectFile Read(string fileName)
        {
            return Read(new Span(fileName));
        }

        public static ScriptObjectFile Read(Span span)
        {
            // header
            var sco = new ScriptObjectFile();
            var stream = new SpanStream(span);
            stream.Skip(6);
            sco.SeparateHeap = stream.ReadByte() != 0;
            stream.Skip(1);
            sco.Number = stream.ReadUInt16();
            UInt16 exportCount = stream.ReadUInt16();
            UInt16 exportOffset = stream.ReadUInt16();
            UInt16 classCount = stream.ReadUInt16();
            UInt16 classOffset = stream.ReadUInt16();
            UInt16 localCount = stream.ReadUInt16();
            UInt16 localOffset = stream.ReadUInt16();

            // exports
            sco.Exports = new List<ExportInfo>(exportCount);
            stream.Seek(exportOffset);
            for (int i = 0; i < exportCount; i++)
            {
                var export = new ExportInfo();
                sco.Exports.Add(export);
                export.Number = stream.ReadUInt16();
                export.Name = ReadString(stream);
            }

            // classes
            sco.Classes = new List<ClassInfo>(classCount);
            stream.Seek(classOffset);
            for (int i = 0; i < classCount; i++)
            {
                var class_ = new ClassInfo();
                sco.Classes.Add(class_);
                class_.Name = ReadString(stream);
                UInt16 propertyCount = stream.ReadUInt16();
                UInt16 methodCount = stream.ReadUInt16();
                class_.Species = stream.ReadUInt16();
                class_.Super = stream.ReadUInt16();
                if (propertyCount != 0xffff)
                {
                    class_.HasNameProperty = true;
                    class_.Properties = new List<PropertyInfo>(propertyCount);
                    for (int j = 0; j < propertyCount; j++)
                    {
                        var property = new PropertyInfo();
                        class_.Properties.Add(property);
                        property.Selector = stream.ReadUInt16();
                        property.Value = stream.ReadUInt16();
                    }
                }

                class_.Methods = new List<UInt16>(methodCount);
                for (int j = 0; j < methodCount; j++)
                {
                    class_.Methods.Add(stream.ReadUInt16());
                }
            }

            // locals
            sco.Locals = new List<string>(localCount);
            stream.Seek(localOffset);
            for (int i = 0; i < localCount; i++)
            {
                sco.Locals.Add(ReadString(stream));
            }

            return sco;
        }

        static string ReadString(SpanStream stream)
        {
            // 16-bit length followed by nul-terminated string.
            // length includes nul.
            UInt16 length = stream.ReadUInt16();
            var s = stream.ReadString(length - 1);
            stream.Skip(1); // nul
            return s;
        }

        public byte[] Generate()
        {
            var stream = new MemoryStream();
            var sco = new BinaryWriter(stream);
            sco.Write('S');
            sco.Write('C');
            sco.Write('O');
            sco.Write((byte)0x01);
            sco.Write((byte)0x00);
            sco.Write((byte)0x00);
            sco.Write((byte)(SeparateHeap ? 1 : 0));
            sco.Write((byte)0x00);
            sco.Write((UInt16)Number);
            sco.Write((UInt16)Exports.Count);
            sco.Write((UInt16)0); // export offset (fill in later)
            sco.Write((UInt16)Classes.Count);
            sco.Write((UInt16)0); // class offset (fill in later)
            sco.Write((UInt16)Locals.Count);
            sco.Write((UInt16)0); // fill in later

            long exportOffset = (Exports.Count == 0) ? 0 : stream.Position;
            for (int i = 0; i < Exports.Count; i++)
            {
                sco.Write(Exports[i].Number);
                WriteString(sco, Exports[i].Name);
            }
            long classOffset = (Classes.Count == 0) ? 0 : stream.Position;
            for (int i = 0; i < Classes.Count; i++)
            {
                WriteString(sco, Classes[i].Name);
                sco.Write((UInt16)(Classes[i].HasNameProperty ? Classes[i].Properties.Count : 0xffff));
                sco.Write((UInt16)Classes[i].Methods.Count);
                sco.Write(Classes[i].Species);
                sco.Write(Classes[i].Super);
                if (Classes[i].HasNameProperty)
                {
                    for (int j = 0; j < Classes[i].Properties.Count; j++)
                    {
                        sco.Write(Classes[i].Properties[j].Selector);
                        sco.Write(Classes[i].Properties[j].Value);
                    }
                }
                foreach (UInt16 method in Classes[i].Methods)
                {
                    sco.Write(method);
                }
            }
            long localOffset = (Locals.Count == 0) ? 0 : stream.Position;
            for (int i = 0; i < Locals.Count; i++)
            {
                WriteString(sco, Locals[i]);
            }

            // fill in the offsets
            sco.Seek(0x0c, SeekOrigin.Begin);
            sco.Write((UInt16)exportOffset);
            sco.Seek(0x10, SeekOrigin.Begin);
            sco.Write((UInt16)classOffset);
            sco.Seek(0x14, SeekOrigin.Begin);
            sco.Write((UInt16)localOffset);

            byte[] buffer = stream.ToArray();
            sco.Dispose();
            stream.Dispose();
            return buffer;
        }

        static void WriteString(BinaryWriter w, string s)
        {
            w.Write((UInt16)(s.Length + 1));
            for (int i = 0; i < s.Length; i++)
            {
                w.Write((byte)s[i]);
            }
            w.Write((byte)0x00);
        }
    }
}
