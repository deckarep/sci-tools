using System;
using System.Collections.Generic;
using System.Linq;

// ScriptL - Script resource format used in Large-Model SCI (LSCI)
//    by The Sierra Network (TSN) / ImagiNation Network (INN)
//
// This is a significantly different resource format. It's an evolution of the
// SCI0 block-based format. "Large" either refers to this format's support of
// scripts greater than 64k, or that it's networked. I haven't seen any scripts
// that big, so that may just be a byproduct of making the format net-safe.
//
// LSCI stores references to objects and functions in IDs instead of offsets.
// An ID is an index into the list of blocks in the script. By not using 16-bit
// offsets, scripts can be larger than 64k. I'm speculating here, but this
// would also make it safe to pass IDs to and from the server as long as the
// script had the expected block list, even if the version was different.
// They also removed the address-of operator from the language, and the lea
// opcode that supported it.
//
// u16 block count
//     block list
// block:
// u8  block type
// u16 block content length
//     block content
// --- if the block type supports relocations ---
// u16 relocation count
//     relocation table [ u16 offset ]
//
// - Relocation tables contain offsets relative to the block content.
// - Relocation values are indexes into the script's block list
//   - lofsa 0001 means load the string/object/said at blockList[1]
// - Variables can be int's or id's, and the "dictionary" block holds
//   bits that define these. keywords "int" and "id" set these.
//   I don't know anything about this yet.
// - Script files are now called modules.
//   - script# keyword is now module#
//   - -script- selector is now -module- and it is the first property.
//
// TODO:
// synonyms [ haven't seen any yet ]
// id/int bitfields on locals and class property dictionaries
//
// I added this parser for fun a few months after finishing the decompiler,
// but I don't really know or care about INN/TSN. There are a lot of TODOs
// in this code, but it handled all the LSCI things I had. I don't know if
// they matter or not; I've never even seen INN/TSN.

namespace SCI.Resource
{
    public class ScriptL
    {
        public int Number;
        public Span Span;

        public List<BlockL> Blocks;

        public List<Local> Locals;
        public List<Export> Exports;
        public List<String> Strings;
        public List<Said> Saids;
        // Synonyms
        public List<ObjectL> Objects;

        public ScriptL(int number, Span span, Vocab vocab = null)
        {
            Number = number;
            Span = span;
            var stream = new SpanStream(span);

            Blocks = ReadBlockList(stream);

            Locals = ReadLocals(stream, Blocks);
            Exports = ReadExports(stream, Blocks);
            Strings = ReadStrings(stream, Blocks);
            Objects = ReadObjects(stream, Blocks, Strings);
            Saids = ReadSaids(stream, Blocks, vocab); // untested, i haven't seen any yet?
        }

        // TODO: id/int bitfields
        //
        // Using arena script 910 as example.
        // RBINVITE.SC, rbInviteRoom.sc.
        //
        // The remaining bytes in the block are a bitfield that indicates
        // which locals are id's (1) and which are int's (0).
        // This script has a listing file with it to confirm this.
        // 46 variables, 45 in source, but compiler adds var count as local0
        // You need 6 bytes for 48 bits, so that's how many extras there are.
        // Most significant bit of the bit field is for local0, and on it goes.
        static List<Local> ReadLocals(SpanStream stream, IReadOnlyList<BlockL> blocks)
        {
            var locals = new List<Local>();

            // there can be multiple sequential locals blocks. SL script 775
            foreach (var block in blocks.Where(b => b.Type == BlockTypeL.Locals))
            {
                stream.Seek(block.Position);

                // In LSCI, the local count is itself a local variable.
                // The first variable in source code is really local1.
                // The id/int bitfield includes local0, so first is always 0.
                UInt16 localCount = stream.ReadUInt16();
                stream.Seek(stream.Position - 2); // rewind
                for (int i = 0; i < localCount; i++)
                {
                    var local = new Local();
                    local.Position = stream.Position16;
                    local.Value = stream.ReadUInt16();
                    if (block.Relocations.Any(r => r == local.Position - block.Position))
                    {
                        var valueBlock = blocks[local.Value];
                        if (valueBlock.Type == BlockTypeL.String)
                        {
                            local.Type = LocalType.String;
                        }
                        else if (valueBlock.Type == BlockTypeL.Said)
                        {
                            local.Type = LocalType.Said;
                        }
                    }
                    locals.Add(local);
                }
            }
            return locals;
        }

        static List<Export> ReadExports(SpanStream stream, IReadOnlyList<BlockL> blocks)
        {
            var block = blocks.SingleOrDefault(b => b.Type == BlockTypeL.Exports);
            if (block == null) return new List<Export>(0);

            stream.Seek(block.Position);
            int exportCount = stream.ReadUInt16() + 1; // weird, also there is a leftover terminator entry
            var exports = new List<Export>(exportCount);
            for (int i = 0; i < exportCount; i++)
            {
                var export = new Export();
                export.Position = stream.Position16;
                UInt16 exportValue = stream.ReadUInt16();
                export.Value = exportValue;
                if (block.Relocations.Any(r => r == export.Position - block.Position))
                {
                    var valueBlock = blocks[exportValue];
                    if (valueBlock.Type == BlockTypeL.Class || valueBlock.Type == BlockTypeL.Instance)
                    {
                        export.Type = ExportType.Object;
                    }
                    else if (valueBlock.Type == BlockTypeL.Code)
                    {
                        export.Type = ExportType.Code;
                    }
                    else if (exportValue != 0) // 0 for empty slots
                    {
                        throw new Exception("Unexpected exported block type: " + valueBlock);
                    }
                }
                exports.Add(export);
            }
            return exports;
        }

        static List<String> ReadStrings(SpanStream stream, IReadOnlyList<BlockL> blocks)
        {
            var strings = new List<String>();
            foreach (var block in blocks.Where(b => b.Type == BlockTypeL.String))
            {
                // 01 00      I don't know what this means
                // 05 00      5 string length, 1 too long
                // s t r \0   4 byte null-terminated string
                stream.Seek(block.Position);
                var str = new String();
                str.Position = stream.Position16;
                UInt16 stringCount = stream.ReadUInt16();
                if (stringCount != 1) throw new Exception("Unexpected LSCI string count: " + stringCount);

                // clamp the advertised string length to the block length.
                // this will still include the null terminator, ReadString() trims that.
                str.Length = stream.ReadUInt16();
                str.Length = Math.Min(str.Length, (UInt16)(block.Position + block.Length - stream.Position));

                str.Text = stream.ReadString(str.Length);
                strings.Add(str);
            }
            return strings;
        }

        static List<BlockL> ReadBlockList(SpanStream stream)
        {
            UInt16 blockCount = stream.ReadUInt16();
            // i don't know the purpose of the high bit in the block count.
            // i've seen $8001 and $8002, followed by empty type zeros, followed
            // by junk that gets ignored by using the small block count.
            // ignoring the whole script if high bit is set would do the same.
            blockCount &= 0x7fff;
            var blocks = new List<BlockL>(blockCount);

            while (blocks.Count < blockCount)
            {
                var block = new BlockL();
                block.Type = (BlockTypeL)stream.ReadByte();
                block.Length = stream.ReadUInt16();
                block.Position = (UInt32)stream.Position;

                stream.Skip(block.Length);

                // Each block gets its own relocation table appended to it,
                // as long as it's a type that supports this.
                switch (block.Type)
                {
                    case BlockTypeL.Instance:   // string/said property
                    case BlockTypeL.Class:      // string/said property
                    case BlockTypeL.Code:       // loadID/pushID/call
                    case BlockTypeL.Locals:     // string/said initial value
                    case BlockTypeL.Exports:    // exported object/code
                    case BlockTypeL.Dictionary: // i don't know; its a list of selectors
                        {
                            UInt16 relocationCount = stream.ReadUInt16();
                            block.Relocations = new UInt16[relocationCount];
                            for (int i = 0; i < relocationCount; i++)
                            {
                                block.Relocations[i] = stream.ReadUInt16();
                            }
                        }
                        break;
                    default:
                        block.Relocations = new UInt16[0];
                        break;
                }

                blocks.Add(block);
            }
            return blocks;
        }

        static List<ObjectL> ReadObjects(SpanStream stream, IReadOnlyList<BlockL> blocks, IReadOnlyList<String> strings)
        {
            var objectBlocks = blocks.Where(b => b.Type == BlockTypeL.Class || b.Type == BlockTypeL.Instance).ToList();
            var objects = new List<ObjectL>(objectBlocks.Count);
            foreach (var block in objectBlocks)
            {
                var obj = ReadObject(stream, block, blocks, strings);
                objects.Add(obj);
            }
            return objects;
        }

        static ObjectL ReadObject(SpanStream stream, BlockL block, IReadOnlyList<BlockL> blocks,
                                  IReadOnlyList<String> strings)
        {
            var obj = new ObjectL();
            obj.Position = block.Position;
            obj.IsClass = (block.Type == BlockTypeL.Class);
            obj.Block = block;

            // property count is the object's second property
            UInt16 propertyCount = stream.GetUInt16((int)block.Position + 2);
            obj.Properties = new List<PropertyL>(propertyCount);
            stream.Seek(block.Position);
            for (int i = 0; i < propertyCount; ++i)
            {
                var property = new PropertyL();
                property.Position = (UInt32)stream.Position;
                property.Value = stream.ReadUInt16();

                // string and said properties are easy to detect.
                // they have a relocation and their value is a block index.
                if (block.Relocations.Any(r => r == property.Position - block.Position))
                {
                    var valueBlock = blocks[property.Value];
                    if (valueBlock.Type == BlockTypeL.String)
                    {
                        property.String = strings.First(s => s.Position == valueBlock.Position);
                    }
                    // TODO: else if Said, else throw
                }

                obj.Properties.Add(property);
            }

            obj.Name = (obj.Properties.Count >= 6) ? obj.Properties[5].String?.Text : null;

            // method dictionary. each Code value is a block index
            UInt16 methodCount = stream.ReadUInt16();
            obj.Methods = new List<Method0>();
            for (int i = 0; i < methodCount; i++)
            {
                var method = new Method0();
                method.SelectorPosition = (UInt16)stream.Position;
                method.Selector = stream.ReadUInt16();
                obj.Methods.Add(method);
            }
            for (int i = 0; i < methodCount; i++)
            {
                obj.Methods[i].CodePosition = stream.Position16;
                obj.Methods[i].Code = stream.ReadUInt16();
            }

            // class property dictionary is in the next Dictionary block.
            // Dictionary blocks have relocations, not sure why.
            if (obj.IsClass)
            {
                var classDictBlock = blocks.First(b => b.Type == BlockTypeL.Dictionary && b.Position > block.Position);
                if (propertyCount > (classDictBlock.Length / 2)) throw new Exception("Dictionary block is too small");

                stream.Seek(classDictBlock.Position);
                obj.ClassPropertySelectors = new List<UInt16>(propertyCount);
                for (int i = 0; i < propertyCount; i++)
                {
                    obj.ClassPropertySelectors.Add(stream.ReadUInt16());
                }
                // TODO: read id/integer bitfield here, not that I'm using it
            }

            return obj;
        }

        static List<Said> ReadSaids(SpanStream stream, IReadOnlyList<BlockL> blocks, Vocab vocab)
        {
            // don't bother if there is no vocab resource
            if (vocab == null) return new List<Said>();

            var saidBlocks = blocks.Where(b => b.Type == BlockTypeL.Said).ToList();
            var saids = new List<Said>(saidBlocks.Count);
            foreach (var block in saidBlocks)
            {
                // i haven't upgraded Said class to handle 32-bit, hoping i don't need to.
                if (block.Position > UInt16.MaxValue) throw new Exception("Said is located beyond 64k");

                stream.Seek(block.Position);
                string saidText = SaidParser.Parse(stream, vocab);
                var said = new Said
                {
                    Position = (UInt16)block.Position,
                    Length = (UInt16)(stream.Position - block.Position),
                    Text = saidText
                };
                saids.Add(said);
            }
            return saids;
        }

        public override string ToString()
        {
            string description = "LSCI Script " + Number;
            foreach (var export in Exports.Where(e => e.Type == ExportType.Object))
            {
                var objBlock = Blocks[(UInt16)export.Value];
                var obj = Objects.FirstOrDefault(o => o.Position == objBlock.Position);
                if (obj != null)
                {
                    return description + " - " + obj.Name;
                }
            }
            foreach (var cls in Objects.Where(o => o.IsClass))
            {
                return description + " - " + cls.Name;
            }
            return description;
        }
    }

    public class BlockL
    {
        public BlockTypeL Type;
        public UInt32 Position;
        public UInt16 Length;
        public UInt16[] Relocations;

        public override string ToString()
        {
            var s = "[" + Type + "] Position: " + Position.Print() + ", Length: " + Length.Print();
            if (Relocations?.Length > 0)
            {
                s += ", " + Relocations.Length + " Relocations";
            }
            return s;
        }
    }

    public enum BlockTypeL
    {
        Unknown = 0,      // Names in LSC.EXE
                          // ================
        Instance = 2,     // "Object"
        Class = 3,        // "Class"
        Code = 4,         // "Interpreted Code"
        CompiledCode = 5, // "Compiled Code"
        Locals = 6,       // "Variable"
        String = 7,       // "String"
        Said = 8,         // "Said Spec"
        Exports = 9,      // "Dispatch"
        Synonyms = 10,    // "Synonyms"
        Dictionary = 11,  // "Dictionary"
    }

    public class ObjectL
    {
        public UInt32 Position; // points at block content
        public bool IsClass;    // from block type
        public string Name;     // from fourth property (if present and not FF FF)

        public BlockL Block;

        public List<PropertyL> Properties;
        public List<UInt16> ClassPropertySelectors; // classes only
        public List<Method0> Methods;

        public UInt16 Species { get { return Properties[2].Value; } } // Class: class number, Instance: class number
        public UInt16 SuperClass { get { return Properties[3].Value; } } // Class: super class number, Instance: class number
        public UInt16 Info { get { return Properties[4].Value; } } // I don't know why we care about this in SCI0

        public override string ToString()
        {
            return Name + (IsClass ? " (Class)" : " (Object)") + " at: " + Position.Print();
        }
    }

    // The first six properties are hard-coded
    enum PropertyIndexL
    {
        Module = 0,
        Size = 1, // property count
        Class = 2,
        Super = 3,
        Info = 4,
        NameHeapOffset = 5, // FF FF if there is no string
    }

    public class PropertyL
    {
        public UInt32 Position;
        public UInt16 Value;
        public String String;
        public Said Said;

        public override string ToString()
        {
            return Value.Print() + (String != null ? (" " + String.Text) : "");
        }
    }
}
