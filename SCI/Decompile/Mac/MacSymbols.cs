using System.Collections.Generic;
using System.Linq;
using SCI.Resource;

// Three Mac games were compiled without object names:
//
//   - Qfg1Vga
//   - Lsl6 (SCI16, also known as floppy version or low-res version)
//   - Hoyle4
//
// I have figured out almost all of the names from DOS versions.
// This is the result of a lot of matching code that can thankfully
// be retired. I manually took care of the last few, job done.
//
// The result is a list of classes and instances with a script
// number and index and object name.
//
// The decompiler uses this to name the nameless objects
// correctly from the start. This takes place in Symbols.cs.
// MacSymbols' job is to provide two dictionaries for it to use.

namespace SCI.Decompile.Mac
{
    class MacSymbols
    {
        public readonly IReadOnlyDictionary<Id, string> Classes;
        public readonly IReadOnlyDictionary<Id, string> Instances;

        public MacSymbols(Game game)
        {
            var script0 = game.Scripts.FirstOrDefault(s => s.Number == 0);
            if (script0 != null && script0.Span.Endian == Endian.Big &&
                game.ByteCodeVersion == ByteCodeVersion.SCI0_11 &&
                game.ScriptFormat == ScriptFormat.SCI11)
            {
                var gameObject = script0.GetExportedObject(0);
                if (IsQfg1VgaMac(game, gameObject))
                {
                    Classes = Qfg1VgaMacSymbols.Classes;
                    Instances = Qfg1VgaMacSymbols.Instances;
                }
                else if (IsLsl6Mac(gameObject))
                {
                    Classes = Lsl6MacSymbols.Classes;
                    Instances = Lsl6MacSymbols.Instances;
                }
                else if (IsHoyle4Mac(gameObject))
                {
                    Classes = Hoyle4MacSymbols.Classes;
                    Instances = Hoyle4MacSymbols.Instances;
                }
            }
        }

        static bool IsQfg1VgaMac(Game game, Object gameObject)
        {
            if (gameObject.Name == null || gameObject.Name == "Glory")
            {
                // signature is script 814 with 33 exported procedures.
                // there are 34 exports though, export 4 is invalid
                var script = game.Scripts.FirstOrDefault(s => s.Number == 814);
                if (script != null &&
                    script.Exports.Count == 34 &&
                    script.Exports.All(e => e.Type == ExportType.Code ||
                                            e == script.Exports[4]))
                {
                    return true;
                }
            }
            return false;
        }

        static bool IsLsl6Mac(Object gameObject)
        {
            // they didn't strip the game object name
            return (gameObject.Name == "LSL6");
        }

        static bool IsHoyle4Mac(Object gameObject)
        {
            if (gameObject.Name == null || gameObject.Name == "hoyle4")
            {
                // signature is script 0 having 9 exports and 750 globals
                var script = gameObject.Script;
                if (script.Exports.Count == 9 &&
                    script.Locals.Count == 750)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Id
    {
        public readonly int Script;
        public readonly int Index;

        public Id(int s, int i)
        {
            Script = s;
            Index = i;
        }

        public override string ToString()
        {
            return Script + ", " + Index;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Id id = (Id)obj;
            return Script.Equals(id.Script) &&
                   Index.Equals(id.Index);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 19 + Script.GetHashCode();
                hash = hash * 19 + Index.GetHashCode();
                return hash;
            }
        }
    }
}
