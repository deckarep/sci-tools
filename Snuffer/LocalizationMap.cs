using System;
using System.Collections.Generic;
using System.IO;

// A localization map is a text file of directory names that maps a localized
// version of a game to its closest English version.
//
// This allows annotators to use English messages when creating comments in the
// decompiled scripts of localized versions. If localized messages were used,
// then it would be impractical to diff the scripts against the originals.
// The only purpose of decompiling localized versions is to see what scripts
// were changed. See: eco1-spanish, phant1-italian
//
// Example file:
//
// ; comment
// sq6-dos-french-1.0 : sq6-dos-1.1
// sq6-dos-german-1.0 : sq6-dos-1.1

namespace Snuffer
{
    public class LocalizationMap : Dictionary<string, string>
    {
        public LocalizationMap(string mapFile)
        {
            var lines = File.ReadAllLines(mapFile);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].TrimStart();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith(";")) continue;

                var parts = line.Split(new[] { ':' });
                if (parts.Length != 2) throw new Exception("Can't parse: " + line);

                Add(parts[0].Trim(), parts[1].Trim());
            }
        }
    }
}
