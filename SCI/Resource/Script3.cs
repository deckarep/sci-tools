using System;
using System.Collections.Generic;
using System.Linq;

// Script3: The final generation of the Script resource format.
//
// - Only used in SCI3 games, so only 4 PC games. No Mac.
// - Single resource again, no more separate heap and hunk.
// - Supports 32 bit offsets. (larger scripts)
// - Some things are dword aligned. (that's new)
// - Large resource format; wasteful compared to earlier ones.
//   Didn't matter, at this point everything was on CD. More RAM.
// - Simpler format to parse, at least for me.
// - All bytecode is still in one buffer.
// - Funky "selector bank" format means that 2 out of every 32
//   selector values are reserved and can never appear in SCI3.
// - Relocations are more complicated. All lofsa instructions
//   have zero for their 16-bit operand and rely on relocations.
//   That's a script-patching limitation; can't move them.
//   (Well, you'd have to patch the relocation table too.)
// - Property instructions (pToa) now use selectors as their
//   operator instead of an internal offset. That's simpler!
// - There is no Mac SCI3. Mac versions of the four SCI3 games
//   use Script11 and other older formats. If these games had
//   large enough scripts that required the expanded capacity
//   of SCI3, that would have been a porting problem.

namespace SCI.Resource
{
    public class Script3
    {
        public int Number;
        public Span Span;

        public UInt32 ByteCodePosition;    // zero when there's no bytecode (Phant2 script 4522)
        public UInt32 StringsPosition;     // zero when string count is zero
        public UInt32 RelocationsPosition; // EOF when there are no relocations (Lighthouse 2.0 script 64916)

        public List<Relocation3> Relocations;
        public List<Export> Exports;
        public List<Local> Locals;
        public List<Object3> Objects;
        public List<String> Strings;

        public UInt32 ByteCodeLength;

        public override string ToString()
        {
            string description = "SCI3 Script " + Number;
            foreach (var export in Exports.Where(e => e.Type == ExportType.Object))
            {
                var obj = Objects.FirstOrDefault(o => o.Position == export.Value);
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

        public Script3(int number, Span span)
        {
            Number = number;
            Span = span;

            var stream = new SpanStream(span);

            ByteCodePosition = stream.ReadUInt32LE();
            StringsPosition = stream.ReadUInt32LE();
            RelocationsPosition = stream.ReadUInt32LE();
            UInt16 localCount = stream.ReadUInt16LE();
            UInt16 objectCount = stream.ReadUInt16LE();
            UInt16 stringCount = stream.ReadUInt16LE();
            UInt16 relocationCount = stream.ReadUInt16LE();
            UInt16 exportCount = stream.ReadUInt16LE();

            // relocations - detour to get these first for objects and exports
            Relocations = new List<Relocation3>(relocationCount);
            if (relocationCount > 0)
            {
                int previousPosition = stream.Position;
                stream.Seek(RelocationsPosition);
                for (UInt16 i = 0; i < relocationCount; ++i)
                {
                    var relocation = new Relocation3();
                    relocation.Position = (UInt32)stream.Position;
                    relocation.Offset = stream.ReadUInt32LE();
                    relocation.Value = stream.ReadUInt32LE();
                    Relocations.Add(relocation);

                    // two junk bytes
                    stream.Skip(2);
                }
                stream.Seek(previousPosition);
            }

            // exports
            Exports = new List<Export>(exportCount);
            for (UInt16 i = 0; i < exportCount; ++i)
            {
                var export = new Export();
                export.Position = stream.Position16;
                export.Value = stream.ReadUInt16LE();
                Exports.Add(export);
                // determine type later after parsing objects
            }

            // local variables (dword-aligned)
            stream.SeekToAlignment(4);
            Locals = new List<Local>(localCount);
            for (UInt16 i = 0; i < localCount; ++i)
            {
                var local = new Local();
                local.Position = stream.Position16;
                local.Value = stream.ReadUInt16LE();
                Locals.Add(local);
            }

            // strings (each is dword aligned)
            // detour to get these before objects
            Strings = new List<String>(stringCount);
            if (stringCount != 0)
            {
                int previousPosition = stream.Position;
                stream.Seek(StringsPosition);
                for (UInt16 i = 0; i < stringCount; ++i)
                {
                    var str = new String();
                    str.Position = (UInt32)stream.Position;
                    str.Text = stream.ReadString();
                    str.Length = (UInt16)(stream.Position - str.Position - 1);
                    Strings.Add(str);

                    stream.SeekToAlignment(4);

                    // lsl7-ru: they patched some strings to smaller ones and left
                    // a lot of null bytes in between. skip swaths of nulls.
                    // TODO: does this break empty strings?
                    while (stream.Position + 4 < RelocationsPosition &&
                           stream.GetUInt32LE(stream.Position) == 0)
                    {
                        stream.Skip(4);
                    }
                }
                stream.Seek(previousPosition);
            }

            // objects (each object is dword aligned)
            Objects = new List<Object3>(objectCount);
            for (UInt16 i = 0; i < objectCount; ++i)
            {
                stream.SeekToAlignment(4);
                var obj = ReadObject(stream, Strings, Relocations);
                Objects.Add(obj);
            }

            // byte code (just calculate the length)
            if (ByteCodePosition != 0)
            {
                // calculate bytecode length by identifying the section that comes after it.
                // this is more cautious than it needs to be since i believe that relocations
                // always follow strings and also even when there are no relocations, the
                // relocations position is EOF (Lighthouse 2.0 script 64916). Still, I'm
                // not relying on either of those.
                UInt32 byteCodeEndPosition = (UInt32)span.Length;
                if (stringCount > 0) byteCodeEndPosition = Math.Min(StringsPosition, byteCodeEndPosition);
                if (relocationCount > 0) byteCodeEndPosition = Math.Min(RelocationsPosition, byteCodeEndPosition);

                ByteCodeLength = byteCodeEndPosition - ByteCodePosition;
            }

            // exports again: identify the types now that we have relocations and objects
            for (int i = 0; i < Exports.Count; ++i)
            {
                var export = Exports[i];

                // all exports for objects have relocations.
                // i have yet to see a code export with a relocation. i don't know if that was possible,
                // or just something that never occurred because no scripts were ever big enough.
                // i think relocations for code exports must have been a possibility, otherwise you
                // wouldn't be able to export a function that appeared after the 64k barrier in bytecode.
                // for now i'm assuming that a relocation means an object and throwing if there is no object.
                var relocation = Relocations.FirstOrDefault(r => r.Offset == export.Position);
                if (relocation != null)
                {
                    export.Type = ExportType.Object;

                    if (Objects.All(o => o.Position != relocation.Value))
                    {
                        // if there are indeed code exports with relocations, this would throw
                        throw new Exception("Export has relocation but it doesn't point to an object: " + relocation);
                    }
                }
                else
                {
                    // SCI3 code exports are offsets relative to the bytecode buffer, instead
                    // of offsets relative to the entire resource as in earlier SCI scripts.
                    // This means that zero is now a valid export value. It wasn't before,
                    // except sometimes, so earlier script formats had invalid exports with zero.
                    // Lighthouse 2.0 script 400 export 1 is a procedure at bytecode position 0.
                    //
                    // SCI3 still has invalid exports. Lighthouse 2.0 script 1 export 0 is 0xBADC,
                    // which finally seems intentional. This is basic invalid export detection during
                    // binary parsing. Script.DiscoverFunctions() assumes the binary parser has at
                    // least guaranteed that each offset is in bounds.
                    export.Type = ExportType.Code;
                    if (export.Value >= ByteCodeLength)
                    {
                        export.Type = ExportType.Invalid;
                        //Log.Debug("Script: " + Number + " Invalid Export: " + i + " Value: " + export.Value.ToString("X4"));
                    }

                    // i want to know if there are any invalid exports with zero, even though zero can be valid.
                    // if bytecode starts with an object method then we'll know because we'll have a pointer to it.
                    if (Objects.Any(o => o.Methods.Any(m => m.Code == export.Value)))
                    {
                        throw new Exception("Export is invalid, it points to a method offset: " + export.Value);
                    }
                }
            }

            IdentifyLocalTypes(Locals, Relocations, Strings);
        }

        static Object3 ReadObject(SpanStream stream, IReadOnlyList<String> strings, IReadOnlyList<Relocation3> relocations)
        {
            // Object3 Format:
            // 16 byte header
            // 256 byte selector bank (table of 256 entries)
            // list of 64 byte selector groups
            int objectPosition = stream.Position;

            UInt16 objectId = stream.ReadUInt16();
            if (objectId != 0x1234)
            {
                throw new Exception("Invalid object id");
            }

            var obj = new Object3();
            obj.Position = (UInt32)objectPosition;

            UInt16 objectSize = stream.ReadUInt16LE();

            var objectSpan = stream.Data.Slice(objectPosition, objectSize);
            var objectStream = new SpanStream(objectSpan);

            objectStream.Seek(4);
            obj.Species = objectStream.ReadUInt16LE();
            objectStream.Skip(2); // "handle to script node for the object"
            obj.Super = objectStream.ReadUInt16LE();
            obj.Info = objectStream.ReadUInt16LE();
            objectStream.Skip(4); // 4 bytes of padding

            // -----------------------------------------------------------
            // Selector Bank: 256 byte table. Each entry represents 32 selectors.
            // If the entry is zero then the object doesn't have any of those
            // 32 selectors. Otherwise, the entry is a one-based index into the
            // array of groups that follows the selector bank. Each group is
            // 64 bytes and contains the selector definitions for that range of 32.
            // -----------------------------------------------------------
            // 0    u8      1-based index into group for selectors 0-31
            // 1    u8      1-based index into group for selectors 32-63
            // ...
            // 255  u8      1-based index into group for selectors 8160-8191
            // -----------------------------------------------------------
            //
            // -----------------------------------------------------------
            // Group: 64 bytes representing 32 selectors except that the
            // the first 2 selectors are sacrificed as a 4 byte mask that
            // indicates which of the available 30 selectors are properties.
            // This means in SCI3 there are no selectors 0, 1, 32, 33...
            // -----------------------------------------------------------
            // 0    u32     mask that tells which of the next 30 selectors
            //              are properties.
            // 4    u16     selector 2 value (property or code offset or FFFF)
            // 6    u16     selector 3 value ...
            // ...
            // 62   u16     selector 30 value ...
            // -----------------------------------------------------------
            //
            // SCI3 loses the original order that properties were declared in.
            // Instead they're ordered by selector value. I tried sorting
            // by the position of the properties but it didn't change.
            // Unfortunately this makes diffing against Mac (Scrip11) noisy;
            // you'd have to sort both alphabetically.

            // 256 byte selector bank. each entry is an optional index to
            // a selector group with values for that range of 32 selectors.
            // an index of zero indicates that the object has no selectors
            // in that range.
            obj.Properties = new List<Property3>();
            obj.Methods = new List<Method3>();
            for (UInt16 bankIndex = 0; bankIndex < 256; ++bankIndex)
            {
                byte groupIndex = objectStream.GetByte(16 + bankIndex);
                if (groupIndex == 0) continue;

                objectStream.Seek(16 + 256 + (groupIndex - 1) * 64);
                UInt32 typeMask = objectStream.ReadUInt32LE();
                for (int bit = 2; bit < 32; ++bit)
                {
                    UInt32 position = (UInt32)(obj.Position + objectStream.Position);
                    UInt16 selector = (UInt16)(bankIndex * 32 + bit);
                    UInt16 value = objectStream.ReadUInt16LE();
                    if ((typeMask & (1 << bit)) != 0)
                    {
                        var property = new Property3();
                        property.Position = position;
                        property.Selector = selector;
                        property.Value = value;

                        // string properties always have relocations.
                        // they're also the only object properties with relocations.
                        var relocation = relocations.FirstOrDefault(r => r.Offset == property.Position);
                        if (relocation != null)
                        {
                            property.String = strings.FirstOrDefault(s => s.Position == relocation.Value);
                            if (property.String == null)
                            {
                                throw new Exception("Object property has relocation but no detected string");
                            }
                        }
                        obj.Properties.Add(property);
                    }
                    else if (value != 0xffff)
                    {
                        var method = new Method3();
                        method.Position = position;
                        method.Selector = selector;
                        method.Code = value;
                        obj.Methods.Add(method);
                    }
                }
            }

            // the first property is "name", but it isn't guaranteed to exist (Rama script 64948)
            obj.Name = obj.Properties.FirstOrDefault()?.String?.Text ?? "";

            stream.Seek(obj.Position + objectSize);

            return obj;
        }

        // identify locals that point to strings.
        // they will have a relocation and that relocation will point to the string.
        static void IdentifyLocalTypes(IReadOnlyList<Local> locals,
                                       IReadOnlyList<Relocation3> relocations,
                                       IReadOnlyList<String> strings)
        {
            foreach (var local in locals)
            {
                if (relocations.Any(r => r.Offset == local.Position &&
                                         strings.Any(s => s.Position == r.Value)))
                {
                    local.Type = LocalType.String;
                }
            }
        }
    }

    // SCI3 relocations are different. Before, relocations were just an array of offsets.
    // Now we have a map that includes a 32-bit value to use instead of the 16-bit
    // value at the given offset. The 16-bit value is (always?) zero in the resource.
    //
    // Relocations can be:
    // Exports (if the export is an object. but maybe code if the function is at a 32-bit offset?)
    // Object properties (strings. it's just strings.)
    // Bytecode. I scanned SCI3 games and only lofsa instructions have relocations.
    public class Relocation3
    {
        public UInt32 Position; // location of the relocation table entry
        public UInt32 Offset;   // offset to relocate
        public UInt32 Value;    // value to use at relocated offset

        public override string ToString()
        {
            return "Offset: " + Offset.ToString("X8") + " Value: " + Value.ToString("X8");
        }
    }

    public class Object3
    {
        public UInt32 Position;
        public bool IsClass { get { return (Info & 0x8000) != 0; } }
        public string Name;

        public UInt16 Species;
        public UInt16 Super;
        public UInt16 Info;

        public List<Property3> Properties;
        public List<Method3> Methods;
    }

    public class Property3
    {
        public UInt32 Position;
        public UInt16 Selector;
        public UInt16 Value;
        public String String;

        public override string ToString()
        {
            return Value.Print() + (String != null ? (" " + String.Text) : "");
        }
    }

    public class Method3
    {
        public UInt32 Position;
        public UInt16 Selector;  // selector for this method
        public UInt32 Code;      // relative to bytecode buffer

        public override string ToString()
        {
            return "Selector: " + Selector.Print() + ", Code offset: " + Code.Print();
        }
    }
}
