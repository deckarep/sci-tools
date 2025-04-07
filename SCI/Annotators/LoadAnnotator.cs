using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Renames/annotates resource loading function enums

    static class LoadAnnotator
    {
        static Dictionary<int, string> resources = new Dictionary<int, string>
        {
            { 128, "rsVIEW" },
            { 129, "rsPIC" },
            { 130, "rsSCRIPT" },
            { 131, "rsTEXT" },
            { 132, "rsSOUND" },
            { 133, "rsMEMORY" },
            { 134, "rsVOCAB" },
            { 135, "rsFONT" },
            { 136, "rsCURSOR" },
            { 137, "rsPATCH" },

            // SCI1.1
            { 0x8a, "rsBITMAP" },
            { 0x8b, "rsPALETTE" },

            { 0x8d, "rsAUDIO" },
            { 0x8e, "rsSYNC" },
            { 0x8f, "rsMESSAGE" },
            { 0x90, "rsAUDIOMAP" },
            { 0x91, "rsHEAP" },
        };

        static Dictionary<int, string> resources16 = new Dictionary<int, string>
        {
            { 0x8c, "rsCDAUDIO" },
            { 0x92, "rsAUDIO36" },
            { 0x93, "rsSYNC36" },
        };

        static Dictionary<int, string> resources32 = new Dictionary<int, string>
        {
            { 0x8c, "WAVE" },
            { 0x92, "CHUNK" },
            { 0x93, "AUDIO36" },
            { 0x94, "SYNC36" },
            { 0x95, "TRANSLATION" },
            { 0x96, "ROBOT" },
            { 0x97, "VMD" },
        };

        public static void Run(Game game, bool sci32)
        {
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                switch (node.At(0).Text)
                {
                    case "Load":
                    case "Unload":
                    case "ResCheck":
                    case "Lock":
                    case "LoadMany":
                        if (node.At(1) is Integer)
                        {
                            int number = node.At(1).Number;
                            string resource;
                            if (resources.TryGetValue(number, out resource))
                            {
                                (node.At(1) as Integer).SetDefineText(resource);
                            }
                            else if (!sci32 && resources16.TryGetValue(number, out resource))
                            {
                                (node.At(1) as Integer).SetDefineText(resource);
                            }
                            else if (sci32 && resources32.TryGetValue(number, out resource))
                            {
                                node.At(1).Annotate(resource);
                            }
                        }
                        break;
                }
            }
        }
    }
}
