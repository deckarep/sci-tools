using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Script0: First generation of the Script resource format.
//
// - Single resource, no separate Heap or Hunk.
// - Resource is broken up into blocks.
// - Block format is consistent, but in early games the script
//   began with a "header" of a 16-bit local variable count.
//   This was soon replaced with a Local block type so that
//   locals could have an initial value other than zero.
//   I try parsing the new format; if that fails, try the old one.
// - Export size changed. The offsets are either 16 or 32 bits,
//   even though you never need that much. Easily detected.
// - lofsa addresses were initially relative, then absolute.
//   This class doesn't care, that's for a higher layer.
// - Saids and Synonyms only appear in this format.
// - Always little endian, regardless of platform.
// - Platforms: PC, Amiga, Atari ST, Mac, FM-Towns, PC-98.
// - Strings can have a junk byte after their null terminator.
//   This happens after empty strings; maybe others?
//   Handling this properly requires a lot of code, some of
//   which occurs outside this class after the script is parsed.

namespace SCI.Resource
{
    public class Script0
    {
        public int Number;
        public Span Span;

        public bool HasOldHeader;
        public List<Block> Blocks;

        public bool HasWideExports;

        public List<Relocation16> Relocations;
        public List<Local> Locals;
        public List<Export> Exports;
        public List<String> Strings;
        public List<Said> Saids;
        public List<Synonym> Synonyms;

        public List<Object0> Objects;

        public override string ToString()
        {
            string description = "SCI0 Script " + Number;
            foreach (var export in Exports.Where(e => e.Type == ExportType.Object))
            {
                var obj = Objects.FirstOrDefault(o => o.Position == export.Value);
                if (obj != null)
                {
                    return description += " - " + obj.Name;
                }
            }
            foreach (var cls in Objects.Where(o => o.IsClass))
            {
                return description += " - " + cls.Name;
            }
            return description;
        }

        public Script0(int number, Span span, Vocab vocab = null)
        {
            Number = number;
            Span = span;
            var stream = new SpanStream(span);

            // read the block list and detect if this script has an old header.
            // first try reading the list without the old header, since most
            // games don't have it, and if that fails for any reason then
            // read the old header and try again. i've abandoned my values by
            // abusing exception handling this way and i hate that it works.
            // i would prefer to only do this detection on the first script,
            // and let the caller pass the result to subsequent scripts, but
            // pq2-dos-1.001.000 has two scripts with the old header. (46, 47)
            HasOldHeader = false;
            UInt16 oldHeader = 0;
            try
            {
                Blocks = ReadBlockList(number, stream, HasOldHeader);
            }
            catch /*(Exception ex)*/
            {
                HasOldHeader = true;
                oldHeader = stream.GetUInt16(0);
                Blocks = ReadBlockList(number, stream, HasOldHeader);
            }

            Relocations = ReadRelocations(number, stream, Blocks);

            if (!HasOldHeader)
            {
                Locals = ReadLocals(stream, Blocks);
            }
            else
            {
                // SCI0 Early doesn't have local blocks.
                // The "old script header" is the number of locals to allocate and they're all zeroed out
                Locals = new List<Local>(oldHeader);
                for (UInt16 i = 0; i < oldHeader; ++i)
                {
                    Locals.Add(new Local { Position = 0xffff, Value = 0 });
                }
            }

            Exports = ReadExports(stream, Blocks, out HasWideExports, number);
            Strings = ReadStrings(stream, Blocks);
            Saids = ReadSaids(stream, Blocks, vocab);
            Synonyms = ReadSynonyms(stream, Blocks);
            Objects = ReadObjects(stream, Blocks, Strings, Saids, Relocations);

            if (!HasOldHeader)
            {
                IdentifyLocalTypes(Locals, Relocations, Strings, Saids);
            }
        }

        static List<Export> ReadExports(SpanStream stream, IReadOnlyList<Block> blocks, out bool hasWideExports, int scriptNumber)
        {
            var exports = new List<Export>();
            hasWideExports = false;

            // use last export block. scummvm says camelot and others have bogus export blocks
            // before the final real one.
            var exportBlock = blocks.LastOrDefault(b => b.Type == BlockType.Exports);
            if (exportBlock == null) return exports;

            stream.Seek(exportBlock.Position);
            UInt16 exportCount = stream.ReadUInt16();

            // detect export width
            if ((exportBlock.Length - 2) == exportCount * 2)
            {
                hasWideExports = false;
            }
            else if ((exportBlock.Length - 2) == exportCount * 4)
            {
                hasWideExports = true;
            }
            else if ((exportBlock.Length - 2) == (exportCount + 1) * 2)
            {
                // early sci studio had an extra word of padding, see Demo Quest
                hasWideExports = false;
            }
            else
            {
                throw new Exception("Can't detect export width");
            }

            for (UInt16 i = 0; i < exportCount; ++i)
            {
                var export = new Export();
                export.Position = (UInt16)stream.Position;
                if (hasWideExports)
                {
                    UInt32 wideExportValue = stream.ReadUInt32();
                    if (wideExportValue > UInt16.MaxValue)
                    {
                        throw new Exception("Wide export is too wide: " + wideExportValue);
                    }
                    export.Value = (UInt16)wideExportValue;
                }
                else
                {
                    export.Value = stream.ReadUInt16();
                }

                var blockContainingExport = blocks.FirstOrDefault(b => b.Position <= export.Value && export.Value <= b.Position + b.Length);
                switch (blockContainingExport?.Type)
                {
                    case BlockType.Class:
                    case BlockType.Instance:
                        // If this is a valid object export then it will point 8 bytes into the block.
                        // That is the location of the first property, and that's what SCI0 uses for
                        // the offset that exports and lofsa point to. See: Object0.Offset.
                        // pq3-amiga-german-1.000 script 129 export 5 is one of these invalid exports.
                        if (export.Value == blockContainingExport.Position + 8)
                        {
                            export.Type = ExportType.Object;
                        }
                        else
                        {
                            export.Type = ExportType.Invalid;
                        }
                        break;
                    case BlockType.Code:
                        export.Type = ExportType.Code;
                        break;
                    default:
                        // this is redundant but i want to make it clear.
                        // Script.DiscoverFunctions() assumes the binary parser has at
                        // least guaranteed that the offset is in bounds.
                        export.Type = ExportType.Invalid;
                        //Log.Debug("Script: " + scriptNumber + " Invalid Export: " + i + " Value: " + export.Value.ToString("X4"));
                        break;
                }

                exports.Add(export);
            }

            return exports;
        }

        static List<Block> ReadBlockList(int number, SpanStream stream, bool hasOldHeader)
        {
            stream.Seek(hasOldHeader ? 2 : 0);

            var blocks = new List<Block>();
            int maxBlockType = hasOldHeader ? 9 : 10; // if old script header, locals (10) shouldn't exist
            bool abort = false;
            while (!abort)
            {
                UInt16 blockType = stream.ReadUInt16();
                if (blockType == 0)
                {
                    break;
                }

                if (blockType > maxBlockType)
                {
                    throw new Exception("Invalid block type: " + blockType);
                }

                var block = new Block();
                block.Type = (BlockType)blockType;
                block.Length = (UInt16)(stream.ReadUInt16() - 4); // includes header size
                block.Position = stream.Position16;
                blocks.Add(block);

                // hoyle3 unused script 995 has a Code length that goes too far.
                // SCI::Console::printKernelCallsFound() suggests there are more
                if (number == 995 &&
                    block.Type == BlockType.Code &&
                    block.Length == 2946 &&
                    stream.Length == 2632)
                {
                    Log.Debug("Ignoring Hoyle3 script 995 OOB block length");
                    block.Length = (UInt16)(stream.Length - stream.Position);
                    abort = true;
                }

                stream.Skip(block.Length);
            }

            // tolerate a small number of junk bytes at the end of the script.
            // i don't like it, but fan tools produce this. (sq3-spanish)
            int maxJunkBytes = hasOldHeader ? 0 : 4;
            if (stream.Length - stream.Position > maxJunkBytes)
            {
                throw new Exception("End of script not reached. Position: " + stream.Position + " Length: " + stream.Length);
            }

            return blocks;
        }

        static List<Local> ReadLocals(SpanStream stream, IReadOnlyList<Block> blocks)
        {
            var localBlocks = blocks.Where(b => b.Type == BlockType.Locals).ToList();
            if (localBlocks.Count > 1) throw new Exception("Multiple Local Blocks!");

            var locals = new List<Local>();
            foreach (var localBlock in localBlocks)
            {
                if (localBlock.Length < 2) throw new Exception("Local block length too small: " + localBlock.Length);
                if (localBlock.Length % 2 == 1) throw new Exception("Local block length is odd: " + localBlock.Length);

                // locals don't start with a count
                stream.Seek(localBlock.Position);
                for (UInt16 i = 0; i < localBlock.Length; i += 2)
                {
                    var local = new Local();
                    local.Position = (UInt16)stream.Position;
                    local.Value = stream.ReadUInt16();
                    locals.Add(local);
                }
            }

            return locals;
        }

        static List<Relocation16> ReadRelocations(int number, SpanStream stream, IReadOnlyList<Block> blocks)
        {
            // There are two formats for relocation blocks:
            // 1. count (word) followed by relocations (each a word)
            // 2. count (word) followed by 00 00 followed by relocations (each a word)
            //
            // sq1vga script 0 relocation block:
            // 08 00    Block Type: Relocation
            // FA 01    Block Length: 1f6 bytes of block data
            // -- block data --
            // F9 00    Relocation entries: f9
            // 00 00    Unknown
            // .. ..    Relocations (f9 * 2 = 1f2 bytes)
            //
            // ScummVM calls this "an extra null entry at the beginning of the table" but I
            // doubt that description. Seems more likely it's just a property they added to
            // the struct whose purpose hasn't been identified. It would be a runtime value
            // and initialized to zero. Although the comment also says that some scripts
            // within the same game don't have it while others do. (BRAIN, PQ3, KQ5CD)
            // See Script::getRelocationTableSci0Sci21().

            var relocationBlocks = blocks.Where(b => b.Type == BlockType.Relocations).ToList();
            if (relocationBlocks.Count > 1) throw new Exception("Multiple Relocation Blocks"); // lets find out!

            var relocations = new List<Relocation16>();
            foreach (var block in relocationBlocks)
            {
                stream.Seek(block.Position);
                UInt16 count = stream.ReadUInt16();
                if (count * 2 + 2 != block.Length)
                {
                    // count + relocations doesn't match block length.
                    // test if this is a newer version with 00 00 before relocations.
                    UInt16 unknown = stream.ReadUInt16();
                    if (unknown != 0 || count * 2 + 4 != block.Length)
                    {
                        throw new Exception("Relocation count mismatch with block length");
                    }
                }

                for (UInt16 i = 0; i < count; ++i)
                {
                    var relocation = new Relocation16();
                    relocation.Position = (UInt16)stream.Position;
                    relocation.Value = stream.ReadUInt16();
                    relocations.Add(relocation);

                    // sq3 1.052 room 71 (base of volcano) claims three relocations but
                    // the second two are way out of bounds. first one is rm071:name.
                    // scummvm only uses ones that fit within an object's properties,
                    // otherwise ignores.
                    if (relocation.Value >= stream.Length - 1)
                    {
                        bool ignoreError =
                            (number == 71) &&
                            (stream.Length == 0x266) &&
                            (count == 3) &&
                            (i == 1 || i == 2);
                        if (!ignoreError)
                        {
                            Log.Debug(string.Format("Relocation out of bounds: [{0:00}]: {1}", i, relocation.Value.Print()));
                        }
                    }
                }
            }

            return relocations;
        }

        // Reading strings is ambiguous because it assumes that strings are adjacent.
        // That's usually true, but not always. Sometimes there are junk bytes in
        // between the null and the next string, and those junk bytes can be anything.
        // There is no way to detect this purely from the strings block.
        // The most important strings are object names. ReadObject() detects if a name
        // property points into the middle of one of these strings, and if so, it corrects
        // the string. I don't know if we can do that to other string properties, because
        // some have bogus values that are inherited from their superclass, which we don't
        // have access to when parsing an individual script.
        //   SQ1VGA:
        //     703 sarienGuard:lookStr    0e67 {It's one of the Sarien guards.}
        //      61 standingSarien:lookStr 0e67 Bogus, since the offset is to a different script
        // This is common, and the bogus offset can be within a string block, in which case
        // correcting it like "name" would be a disaster.
        // Script.FixupSci0Strings() fixes strings based on lofsa instructions that point within
        // the detected string. Game runs that after all scripts are fully parsed and all
        // functions are detected. Script 255 usually has a junk byte in front of "PrintD".
        static List<String> ReadStrings(SpanStream stream, IReadOnlyList<Block> blocks)
        {
            var stringBlocks = blocks.Where(b => b.Type == BlockType.Strings).ToList();
            if (stringBlocks.Count > 1) throw new Exception("Multiple String Blocks!");

            var strings = new List<String>();
            foreach (var stringBlock in stringBlocks)
            {
                // parse out null terminated strings
                UInt16 currentStringPosition = stringBlock.Position;
                var currentStringText = new StringBuilder();
                for (UInt16 i = stringBlock.Position; i < stringBlock.Position + stringBlock.Length; ++i)
                {
                    byte b = stream.GetByte(i);
                    if (b == 0)
                    {
                        var string0 = new String();
                        string0.Position = currentStringPosition;
                        string0.Length = (UInt16)(i - currentStringPosition);
                        string0.Text = currentStringText.ToString();
                        strings.Add(string0);

                        currentStringPosition = (UInt16)(i + 1);
                        currentStringText.Clear();
                    }
                    else
                    {
                        currentStringText.Append((char)b);
                    }
                }

                // ignore any leftover characters in currentStringText when we
                // reach the end of a string block. they're just junk bytes.
                // i've confirmed this by logging all of these unterminated
                // leftovers. SCI Companion doesn't do this, and so it lists
                // strings in its disassembly that don't exist, and have chars
                // that are from beyond the string block.
                // examples: kq1 script 63, qfg1vga script 944
            }

            return strings;
        }

        static List<Said> ReadSaids(SpanStream stream, IReadOnlyList<Block> blocks, Vocab vocab)
        {
            var saidBlocks = blocks.Where(b => b.Type == BlockType.Saids).ToList();
            if (saidBlocks.Count > 1) throw new Exception("Multiple Said Blocks!");

            // don't bother if there is no vocab resource
            if (vocab == null) return new List<Said>();

            var saids = new List<Said>();
            foreach (var block in saidBlocks)
            {
                stream.Seek(block.Position);
                while (stream.Position < block.Position + block.Length - 1) // - 1 because there's a junk byte
                {
                    UInt16 saidPosition = (UInt16)(stream.Position);
                    string saidText = SaidParser.Parse(stream, vocab);
                    var said = new Said
                    {
                        Position = saidPosition,
                        Length = (UInt16)(stream.Position - saidPosition),
                        Text = saidText
                    };
                    saids.Add(said);
                }
            }

            return saids;
        }

        static List<Synonym> ReadSynonyms(SpanStream stream, IReadOnlyList<Block> blocks)
        {
            var synonymsBlocks = blocks.Where(b => b.Type == BlockType.Synonyms).ToList();
            if (synonymsBlocks.Count > 1) throw new Exception("Multiple Synonyms Blocks!");

            // tuples of UInt16s
            var synonyms = new List<Synonym>();
            foreach (var block in synonymsBlocks)
            {
                stream.Seek(block.Position);
                while (stream.Position < block.Position + block.Length - 4)
                {
                    UInt16 a = stream.ReadUInt16();
                    UInt16 b = stream.ReadUInt16();
                    var synonym = synonyms.FirstOrDefault(s => s.Group == b);
                    if (synonym == null)
                    {
                        synonym = new Synonym { Group = b, Groups = new List<UInt16>() };
                        synonyms.Add(synonym);
                    }
                    synonym.Groups.Add(a);
                }
            }
            return synonyms;
        }

        static List<Object0> ReadObjects(SpanStream stream, IReadOnlyList<Block> blocks,
                                         List<String> strings, IReadOnlyList<Said> saids,
                                         IReadOnlyList<Relocation16> relocations)
        {
            var objectBlocks = blocks.Where(b => b.Type == BlockType.Class || b.Type == BlockType.Instance).ToList();
            var objects = new List<Object0>(objectBlocks.Count);
            foreach (var block in objectBlocks)
            {
                var obj = ReadObject(stream, block, blocks, strings, saids, relocations);
                objects.Add(obj);
            }
            return objects;
        }

        static Object0 ReadObject(SpanStream stream, Block block, IReadOnlyList<Block> blocks,
                                  List<String> strings, IReadOnlyList<Said> saids,
                                  IReadOnlyList<Relocation16> relocations)
        {
            var obj = new Object0();
            obj.Position = block.Position;
            obj.IsClass = (block.Type == BlockType.Class);
            obj.Block = block;

            stream.Seek(block.Position);

            // 0x1234
            UInt16 magicObjectNumber = stream.ReadUInt16();
            if (magicObjectNumber != 0x1234) throw new Exception("Incorrect magic object number: " + magicObjectNumber);

            // "wLocalVarOffset"
            UInt16 localVarOffset = stream.ReadUInt16();

            // "wFunctionSelectorOffset"
            UInt16 functionSelectorOffset = stream.ReadUInt16();

            // "wNumVarSelectors"
            UInt16 propertyCount = stream.ReadUInt16();

            // fixed properties:
            // 0    species
            // 1    super class
            // 2    info
            // 3    name (optional)
            obj.Properties = new List<Property16>();
            for (UInt16 i = 0; i < propertyCount; ++i)
            {
                var property = new Property16();
                property.Position = stream.Position16;
                property.Value = stream.ReadUInt16();

                // detect string or said properties by checking for a relocation and
                // a detected string (or said). without the relocation check, a property
                // whose value just happens to match a string/said location would be
                // incorrectly detected. example: kq5cd script 6 has a "door" of class "Door".
                // Door:closeDoorNumber is 8124 (decimal) and that's also the offset for the
                // string "face" which is the name of an instance. hoyle1 script 501 and other
                // 5xx character scripts all have class 0x56 which is also the offset that
                // their name string is at. SCI Companion doesn't read the relocation table,
                // it only tests against the detected string/said positions, and so its
                // disassembly will show "face" in kq5cd. decompilation turns out fine though?
                if (i == (int)PropertyIndex0.NameHeapOffset) // special handling for "name" property
                {
                    // "name" might not point to a detected string because string detection is imperfect.
                    // but since we know that "name" is a string that can't be inherited, if it points
                    // to the middle of a string then we know that string is prefaced by junk bytes,
                    // (or worse, see below) so we "correct" this by creating a new string.
                    if (relocations.Any(r => r.Value == property.Position))
                    {
                        // find the string object that the name property points within
                        property.String = strings.FirstOrDefault(s => s.Position <= property.Value && property.Value < s.Position + s.Length);
                        
                        // if we found a string object but the name property doesn't point
                        // to the start of the string, then we've got some work to do.
                        if (property.String != null && property.String.Position != property.Value)
                        {
                            // originally i trimmed the property's string, since this is about
                            // trimming junk bytes, but then i started parsing fan translations.
                            // LSL1VGA Russian has properties pointing into the same strings to
                            // re-use them for substrings. now i create a new string object to
                            // allow multiple string objects to overlap. blerg.
                            var oldString0 = property.String;
                            var newString0 = new String();
                            newString0.Position = property.Value;
                            newString0.Length = (UInt16)(oldString0.Length - (newString0.Position - oldString0.Position));
                            newString0.Text = oldString0.Text.Substring((int)(newString0.Position - oldString0.Position));
                            property.String = newString0;
                            strings.Insert(strings.IndexOf(oldString0) + 1, newString0);
                        }
                    }
                }
                else if (i > 3) // properties 0-2 are never pointers
                {
                    // if there's a relocation and a string/said, use the string/said.
                    // ignore properties that don't have a valid string/said, they could be inherited
                    // pointers from their parent, like KQInv\Inv in KQ5 Amiga.
                    // this means we can't detect string properties that start with junk bytes.
                    if (relocations.Any(r => r.Value == property.Position))
                    {
                        property.String = strings.FirstOrDefault(s => s.Position == property.Value);
                        if (property.String == null)
                        {
                            property.Said = saids.FirstOrDefault(s => s.Position == property.Value);
                        }
                    }
                }

                obj.Properties.Add(property);
            }

            if (obj.Properties.Count < 3) throw new Exception("Object has too few properties: " + obj.Properties.Count);

            obj.Name = (obj.Properties.Count >= 4) ? obj.Properties[3].String?.Text : null;

            if (obj.IsClass)
            {
                // class property selectors
                obj.ClassPropertySelectors = new List<UInt16>(propertyCount);
                for (UInt16 i = 0; i < propertyCount; ++i)
                {
                    obj.ClassPropertySelectors.Add(stream.ReadUInt16());
                }
            }

            UInt16 methodCount = stream.ReadUInt16();
            obj.Methods = new List<Method0>();
            for (UInt16 i = 0; i < methodCount; ++i)
            {
                var method = new Method0();
                method.SelectorPosition = (UInt16)stream.Position;
                method.Selector = stream.ReadUInt16();
                obj.Methods.Add(method);
            }

            // two bytes of zero in between the method lists
            UInt16 zero = stream.ReadUInt16();
            if (zero != 0) throw new Exception("Expected zero: " + zero);

            for (UInt16 i = 0; i < methodCount; ++i)
            {
                obj.Methods[i].CodePosition = (UInt16)stream.Position;
                obj.Methods[i].Code = stream.ReadUInt16();

                if (!blocks.Any(b => b.Type == BlockType.Code &&
                                     b.Position <= obj.Methods[i].Code &&
                                     obj.Methods[i].Code <= b.Position + b.Length))
                {
                    throw new Exception("Code doesn't point to a Code block: " + obj.Methods[i].Code);
                }
            }

            // kq4-early has two bytes left. sci companion says it doesn't care about what's leftover
            //int leftoverByteCount = block.Position + block.Length - stream.Position;
            //Log.Debug("    Bytes left: " + leftoverByteCount);

            return obj;
        }

        // final step since we need to know the strings and saids
        static void IdentifyLocalTypes(IReadOnlyList<Local> locals, IReadOnlyList<Relocation16> relocations,
                                       IReadOnlyList<String> strings, IReadOnlyList<Said> saids)
        {
            if (locals.Count == 0) return;
            UInt16 firstLocalPosition = locals.First().Position;
            UInt16 lastLocalPosition = locals.Last().Position;
            foreach (var relocation in relocations.Where(r => firstLocalPosition <= r.Value &&
                                                              r.Value <= lastLocalPosition))
            {
                var local = locals.FirstOrDefault(l => l.Position == relocation.Value);
                if (local == null) continue;

                if (strings.Any(s => s.Position == local.Value))
                {
                    local.Type = LocalType.String;
                }
                else if (saids.Any(s => s.Position == local.Value))
                {
                    local.Type = LocalType.Said;
                }
            }
        }
    }

    // Blocks and BlockTypes are only in SCI0 scripts.
    // * LSCI has blocks and types but they're different.

    public enum BlockType
    {
        Unknown = 0,

        Instance = 1,
        Code = 2,
        Synonyms = 3,
        Saids = 4,
        Strings = 5,
        Class = 6,
        Exports = 7,
        Relocations = 8,
        PreloadTextFlag = 9,
        Locals = 10 // added after KQ4 early
    }

    // Block format:
    // uint16   Type
    // uint16   Length (including the 4 header bytes)
    // data
    public class Block
    {
        public BlockType Type;  // type of block, as defined by the first two bytes of the block header
        public UInt16 Position; // location of the block (resource offset)
        public UInt16 Length;   // length of the block, excluding the header size

        public override string ToString()
        {
            return "[" + Type + "] Position: " + Position.Print() + ", Length: " + Length.Print();
        }
    }

    // The first four properties are hard-coded
    enum PropertyIndex0
    {
        Species = 0,
        Super = 1,
        Info = 2,
        NameHeapOffset = 3 // optional, sometimes there are only three properties
    }

    // Relocations let us detect pointers to objects/strings/saids so that we
    // can disambiguate pointers from integer values. That is not the purpose of
    // relocations, but that is their value to a resource tool; otherwise I'd
    // ignore them. The relocation table is an array of offsets into the script
    // resource. The interpreter copies the script into memory, walks the table,
    // and adjusts the values so that they can be used as pointers. That might
    // be an incomplete description, but it's true enough, and it reflects my
    // level of interest -- I only care about using them for accurate parsing.
    // "Relocations" is the reverse engineering term for these values, according
    // to interpreter source they were called "fixups".
    //
    // Relocations can point to:
    // - local variable:  It's a pointer to a string or a said.
    // - object property: It's a pointer to a string or a said.
    // - bytecode:        It's a lofsa or lofss operand.
    //                    These don't matter; the operand is always a pointer.
    // - export table:    The export is an object in SCI11 and SCI3.
    //                    These don't matter; exported objects are unambiguous.
    //
    // SCI11 has two relocation arrays per script: one for heap, one for hunk.
    // SCI3 uses a lookup table to support 32-bit offsets; it's much different.
    public class Relocation16 // also used by Script11
    {
        public UInt16 Position; // location of the relocation table entry
        public UInt16 Value;    // value at the relocation table entry (resource offset)

        public override string ToString()
        {
            return Value.Print() + " at: " + Position.Print();
        }
    }

    public class Object0
    {
        public UInt16 Position; // points at 0x1234
        public bool IsClass; // from block type
        public string Name;  // from fourth property (it's sometimes missing)

        public Block Block;

        public List<Property16> Properties;
        public List<UInt16> ClassPropertySelectors; // classes only
        public List<Method0> Methods;

        public UInt16 Species    { get { return Properties[0].Value; } } // Class: class number, Instance: class number
        public UInt16 SuperClass { get { return Properties[1].Value; } } // Class: super class number, Instance: class number
        public UInt16 Info       { get { return Properties[2].Value; } } // Class: 0x8000 flag set

        // Script0 format doesn't reference objects by the position of magic 0x1234.
        // Instead, they reference the position of the first property (Species).
        // For example, the export table uses this value.
        public UInt16 Offset { get { return Properties[0].Position; } }

        public override string ToString()
        {
            return Name + (IsClass ? " (Class)" : " (Object)") + " at: " + Position.Print();
        }
    }

    public class Property16 // also used by Script11
    {
        public UInt16 Position;
        public UInt16 Value;
        public String String;
        public Said Said;

        public override string ToString()
        {
            return Value.Print() + (String != null ? (" " + String.Text) : "");
        }
    }

    // Method0 doesn't have a Position because there are two:
    // the position of the selector value and the position of the code value.
    // they're not consecutive; selectors are grouped separately from code offsets.
    public class Method0
    {
        public UInt16 Selector;         // selector for this method
        public UInt16 SelectorPosition; // location of the selector value
        public UInt16 Code;             // location of method bytecode (resource offset)
        public UInt16 CodePosition;     // location of the code value

        public override string ToString()
        {
            return "Selector: " + Selector.Print() + ", Code offset: " + Code.Print();
        }
    }
}
