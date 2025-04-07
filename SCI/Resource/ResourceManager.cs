using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SCI.Resource.Decompressors;

// ResourceManager's job is to take a directory of game files and find all of
// the resources. Callers can then retrieve the raw uncompressed contents of
// any resource by id. A resource's id is a tuple of its type and number.
// Base 36 resource numbers aren't supported yet, because I never needed them.
// This resource manager is conceptually similar to ScummVM's. The goal is to
// support all SCI versions. Mac files (Data#) work if they are in MacBinary
// format or if their resource forks are separated into a separate .rsrc file.
// A directory with no volume files but just patch files also works. (Initially
// that was the only supported format, letting me postpone the pain of dealing
// with maps and volumes.)
//
// ResourceManager knows the minimal SCI version information required to get
// to the resource data. It knows the map version and volume version.
// It also does SCI Companion's view-compression heuristic to determine the
// meaning of the compression algorithm values. It does *not* know endianness,
// but clients can update Span.Endian if they know better. Spans default to
// Unknown which acts like Little. If a Span comes from a Mac resource though,
// it will default to Big.
//
// There are big obstacles to writing a resource manager:
// 1. All the changes to all the SCI formats (maps, volumes, filenames)
// 3. All the combinations of these formats
// 3. Corrupt resources
// 4. Resources produced by third party tools (junk bytes, incorrect values)
//
// These problems are in direct conflict. Determining the version of SCI formats
// means interpreting the bytes in several ways until one is valid, but some
// of these files are actually invalid. If the original game never used a bad
// resource at runtime in a release configuration then it never mattered since
// SSCI never processed it. But if you're processing everything as part of an
// initial analysis, you stumble on every one of these broken resources.
// Thirty party tools can produce legal layouts that are good enough for the
// interpreter to run but have junk bytes that throw off version detection.
//
// I'm foolishly going to document some problems:
//
// - Map entry points to garbage. This is bad. It's easy to detect though.
//   Each map entry should point to a volume entry that starts with the same
//   resource id. If it doesn't, it's bad. Good thing this doesn't happen
//   more often, because my version detection method is based on this match.
//   * SQ4 Beta has a corrupt entry.
//   * KQ6 Mac has truncated debug script 911.scr (or is it hep?)
//   * GK1 comes with a broken SysLogger script (952) that has fucked up methods
//     that report bogus instructions. I assume something is truncated.
//     It doesn't cause ResourceManager any problems, but I would like it
//     to just get ignored at some level.
// - Truncated resource in volume. QFG3 1.1's resource.000 ends with vocab 998
//   but the compressed size it advertises is too big for the volume file.
//   This is used for debugging so it never mattered. ScummVM can't dump it.
//   I added a check to ignore these and write to console but that should
//   be an option.
// - Ambiguous version detection on some message.map files. There's only
//   one resource type (message) and the list is divisible by both lengths.
//   The only unambiguous version detection would be to get the volume
//   involved, but you know, we also don't know which volume version it is.
//   I've "solved" this by using the first detected map version for all
//   subsequent maps in a directory; no further auto-detection.
// - Patch file priority. If there is a patch file in the root directory and
//   another version of the same file in a PATCHES subdirectory, which gets
//   priority? At runtime the interpreter would get that from RESOURCE.CFG,
//   which may not be present, and is unreliable even when it is.

namespace SCI.Resource
{
    public enum ResourceType
    {
        View = 0,
        Pic,
        Script,
        Text,
        Sound,
        Memory,
        Vocab,
        Font,
        Cursor,
        Patch,
        Bitmap,
        Palette,
        CdAudio = 12,
        Wave = 12,
        Audio,
        Sync,
        Message,
        Map,
        Heap,
        Audio36,
        Sync36,
        Translation,  // "currently unsupported" --scummvm

        // SCI2.1+
        Robot,
        VMD,
        Chunk,
        Animation,

        // SCI3
        Etc,
        Duck,
        Clut,
        TGA,
        ZZZ
    }

    public enum ScriptFormat
    {
        Unknown,
        SCI0,
        SCI11,
        SCI3,
        LSCI
    }

    public class ResourceId
    {
        public ResourceType Type { get; private set; }
        public UInt16 Number { get; private set; }

        public ResourceId(ResourceType type, UInt16 number)
        {
            Type = type;
            Number = number;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            ResourceId r = (ResourceId)obj;
            return Type.Equals(r.Type) &&
                   Number.Equals(r.Number);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 19 + Type.GetHashCode();
                hash = hash * 19 + Number.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return Type + " " + Number;
        }
    }

    public class ResourceManager
    {
        public string Name { get; private set; }
        public string Directory { get; private set; }

        Dictionary<ResourceId, Source> resources; // active
        List<ResourceMap> maps;
        List<ResourceVolume> volumes;
        List<MacResourceFork> macForks;
        List<PatchSource> patches;

        CompressionFormat compressionFormat;

        const byte PatchVolumeNumber = 255;

        public ResourceManager(string directory)
        {
            Name = Path.GetFileName(directory);
            Directory = directory;

            resources = new Dictionary<ResourceId, Source>();
            maps = new List<ResourceMap>();
            volumes = new List<ResourceVolume>();
            macForks = new List<MacResourceFork>();
            patches = new List<PatchSource>();

            // try loading resources from map and volume files first.
            // if no maps are found then try mac volumes
            var files = new DirectoryInfo(directory).GetFiles();
            InitializeMapsAndVolumes(files);
            if (!volumes.Any())
            {
                InitializeMacForks(files);
            }

            // always apply patch files, even if there are no volumes
            // root directory has higher priority than patches directory.
            AddPatchFiles(Path.Combine(Directory, "patches"));
            AddPatchFiles(Directory);
        }

        void InitializeMapsAndVolumes(FileInfo[] files)
        {
            // Each game uses only one map version and one volume version.
            // Once the map version is detected in the first map, use it for any
            // others instead of detecting each time. This takes care of maps
            // like message.map that can be ambiguous, at least, without bringing
            // the volume file into the map version detection.

            var detectedMapVersion = MapVersion.Unknown;
            if (Exists(files, "resource.map"))
            {
                // resource.map / resource.### names
                var mapFile = Find(files, "resource.map");

                var map = ResourceMap.Read(mapFile.FullName);
                detectedMapVersion = map.Version;
                maps.Add(map);

                bool considerSplitFiles = (map.Version == MapVersion.SCI11 ||
                                           map.Version == MapVersion.SCI2) &&
                                           map.Entries.All(e => e.VolumeNumber == 0) ||
                                           Exists(files, "resource.p01"); // PQ1VGA map is SCI1_Late

                foreach (var file in files.OrderBy(f => f.Name))
                {
                    byte number;
                    if (ParseExtensionNumber(file.Name, "resource", out number, false))
                    {
                        // if split files are a possibility then ignore everything except resource.000
                        if (considerSplitFiles && number != 0)
                        {
                            continue;
                        }

                        var volume = ResourceVolume.Read(map, file.FullName, number);
                        volumes.Add(volume);
                    }
                }

                // if no resource.### files were found, lets look for resource.p##.
                // these comprise resource.000 split across floppy disks.
                if (!volumes.Any() && considerSplitFiles)
                {
                    var splitVolumeFiles = new List<FileInfo>();
                    foreach (var file in files.OrderBy(f => f.Name))
                    {
                        byte number;
                        if (ParseExtensionNumber(file.Name, "resource", out number, true))
                        {
                            // no gaps allowed. starts with resource.p01
                            if (number != splitVolumeFiles.Count + 1)
                            {
                                splitVolumeFiles.Clear();
                                break;
                            }
                            splitVolumeFiles.Add(file);
                        }
                    }

                    if (splitVolumeFiles.Any())
                    {
                        // concatenate the split files together as resource.000
                        var mergedVolumeFiles = MergeFiles(splitVolumeFiles);
                        var mergedVolume = ResourceVolume.Read(map, 0, mergedVolumeFiles);
                        mergedVolume.Name = "resource.000 [merged]";
                        volumes.Add(mergedVolume);
                    }
                }

                // detect compression format on earlier map versions
                if (detectedMapVersion == MapVersion.SCI11 ||
                    detectedMapVersion == MapVersion.SCI2)
                {
                    compressionFormat = CompressionFormat.SCI1;
                }
                else
                {
                    compressionFormat = DetectCompressionFormat();
                }
            }
            else if (Exists(files, "resmap.000") || Exists(files, "resmap.001"))
            {
                // resmap.### / ressci.###
                foreach (var file in files.OrderBy(f => f.Name))
                {
                    byte number;
                    if (ParseExtensionNumber(file.Name, "resmap", out number, false))
                    {
                        AddMapAndVolume(files, file.Name, "ressci" + file.Extension, number, detectedMapVersion);
                        detectedMapVersion = maps.First().Version;
                    }
                }

                // GK2 patches
                AddMapAndVolume(files, "resmap.pat", "ressci.pat", PatchVolumeNumber, detectedMapVersion);

                // setting for completeness but doesn't matter
                compressionFormat = CompressionFormat.SCI1;
            }

            AddMapAndVolume(files, "message.map", "resource.msg", 0, detectedMapVersion);

            // GK1 CD hi-res resources:
            AddMapAndVolume(files, "alt.map", "resource.alt", 0, detectedMapVersion);

            // KQ7 / PQ4:
            AddMapAndVolume(files, "altres.map", "altres.000", PatchVolumeNumber, detectedMapVersion);

            // add resources from volumes in order of priority, from most important to least:
            // - patch volume    255
            // - resource.msg      0 [ for now ]
            // - resource 000      0
            // - resource.001      1
            // - resource.002 ...  2
            volumes.Sort(delegate (ResourceVolume x, ResourceVolume y)
            {
                if (x.Number == y.Number)
                {
                    // oops i realized "too late" that resource.msg wasn't overriding resource.000
                    // so i'm hacking it in for now, but i need to re-work the priority to be cleaner.
                    // can't set resource.msg to PatchVolumeNumber currently.
                    if (x.Name.Equals("resource.msg", StringComparison.OrdinalIgnoreCase) && !y.Name.Equals("resource.msg", StringComparison.OrdinalIgnoreCase)) return -1;
                    if (!x.Name.Equals("resource.msg", StringComparison.OrdinalIgnoreCase) && y.Name.Equals("resource.msg", StringComparison.OrdinalIgnoreCase)) return 1;
                    // same for resource.alt i guess?
                    if (x.Name.Equals("resource.alt", StringComparison.OrdinalIgnoreCase) && !y.Name.Equals("resource.alt", StringComparison.OrdinalIgnoreCase)) return -1;
                    if (!x.Name.Equals("resource.alt", StringComparison.OrdinalIgnoreCase) && y.Name.Equals("resource.alt", StringComparison.OrdinalIgnoreCase)) return 1;

                    return 0;
                }
                // whoever has a patch volume number comes before whoever doesn't
                if (x.Number == PatchVolumeNumber) return -1;
                if (y.Number == PatchVolumeNumber) return 1;
                // compare against volume number
                return x.Number.CompareTo(y.Number);
            });
            foreach (var volume in volumes)
            {
                for (int e = 0; e < volume.Entries.Count; ++e)
                {
                    // only add a resource if it hasn't already been added
                    var entry = volume.Entries[e];
                    var id = new ResourceId(ToResourceType(entry.Type), entry.Number);
                    if (!resources.ContainsKey(id))
                    {
                        if (entry.DataOffset + entry.PackedSize > volume.Span.Length)
                        {
                            Log.Debug("Truncated resource in " + volume.Name + ": Type " + entry.Type.ToString("X2") + " Number " + entry.Number);
                            volume.Entries.RemoveAt(e);
                            e--;
                            continue;
                        }

                        var compression = GetCompression(entry.Compression);
                        var compressed = volume.Span.Slice(entry.DataOffset, (int)entry.PackedSize);
                        var source = new VolumeSource(volume.Name, compression, compressed, entry.UnpackedSize);
                        resources.Add(id, source);
                    }
                }
            }
        }

        void InitializeMacForks(FileInfo[] files)
        {
            // start at Data1 and read each file until we can't
            for (int i = 1; ; i++)
            {
                var file = Find(files, "Data" + i);
                if (file == null)
                {
                    break;
                }
                macForks.Add(MacResourceFork.Read(file.FullName));
            }
            if (!macForks.Any())
            {
                return;
            }

            var macTypeMap = new Dictionary<string, ResourceType>
            {
                { "V56 ", ResourceType.View },
                { "P56 ", ResourceType.Pic },
                { "SCR ", ResourceType.Script },
                { "TEX ", ResourceType.Text },
                { "SND ", ResourceType.Sound },
                { "VOC ", ResourceType.Vocab },
                { "FON ", ResourceType.Font },
                { "CURS", ResourceType.Cursor },
                { "crsr", ResourceType.Cursor },
                { "Pat ", ResourceType.Patch },
                { "snd ", ResourceType.Sound },
                { "MSG ", ResourceType.Message },
                { "HEP ", ResourceType.Heap },
                // IBIN, MacIconBarPictN
                // IBIS, MacIconBarPictS
                // PICT, MacPict
                { "SYN ", ResourceType.Sync },
                { "SYNC", ResourceType.Sync },
            };

            // detect if there is compression or not
            bool detectionSucceeded = false;
            bool hasCompressionFooter = false;
            foreach (var fork in macForks)
            {
                foreach (var type in fork.Types)
                {
                    if (!(type.Name == "SCR " || type.Name == "HEP ")) continue;
                    foreach (var script in type.Entries)
                    {
                        detectionSucceeded = MacDecompressor.DetectCompressionFooter(script.Span, out hasCompressionFooter);
                        if (detectionSucceeded) break;
                    }
                    if (detectionSucceeded) break;
                }
                if (detectionSucceeded) break;
            }
            if (!detectionSucceeded) throw new Exception("Unable to detect Mac compression");

            foreach (var fork in macForks)
            {
                foreach (var type in fork.Types)
                {
                    // map to SCI type. skip if unknown
                    ResourceType sciType;
                    if (!macTypeMap.TryGetValue(type.Name, out sciType))
                    {
                        continue;
                    }

                    foreach (var entry in type.Entries)
                    {
                        string name = entry.Name + " in " + fork.Name;
                        var id = new ResourceId(sciType, entry.Id);
                        if (!resources.ContainsKey(id))
                        {
                            // TODO: add GK2 pic hard-coded handling, set canBeCompressed
                            // to false on types known to be uncompressed.
                            bool canBeCompressed = hasCompressionFooter;
                            var source = new MacResourceForkSource(name, id, canBeCompressed, entry.Span);
                            resources.Add(id, source);
                        }
                    }
                }
            }
        }

        public bool Has(ResourceType type)
        {
            return resources.Keys.Any(k => k.Type == type);
        }

        public bool Has(ResourceType type, UInt16 number)
        {
            return resources.ContainsKey(new ResourceId(type, number));
        }

        public bool Has(ResourceId id)
        {
            return resources.ContainsKey(id);
        }

        public Span GetResource(ResourceType type, UInt16 number)
        {
            return GetResource(new ResourceId(type, number));
        }

        public Span GetResource(ResourceId id)
        {
            Source source;
            if (resources.TryGetValue(id, out source))
            {
                return source.GetData();
            }
            return null;
        }

        public IEnumerable<ResourceId> GetResources()
        {
            return resources.Keys;
        }

        public IEnumerable<ResourceId> GetResources(ResourceType type)
        {
            return resources.Keys.Where(k => k.Type == type).OrderBy(k => k.Number);
        }

        public IReadOnlyDictionary<ResourceId, Source> GetResourceSources()
        {
            return resources;
        }

        void AddPatchFiles(string directory)
        {
            if (!System.IO.Directory.Exists(directory)) return;

            foreach (var file in System.IO.Directory.GetFiles(directory))
            {
                ResourceId id = PatchFileNames.Parse(file);
                if (id != null)
                {
                    var source = new PatchSource(id, file);
                    patches.Add(source);
                    resources[id] = source;
                }
            }
        }

        static bool Exists(FileInfo[] files, string fileName)
        {
            return Find(files, fileName) != null;
        }

        static FileInfo Find(FileInfo[] files, string fileName)
        {
            return files.FirstOrDefault(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));
        }

        void AddMapAndVolume(FileInfo[] files, string mapFileName, string volumeFileName, byte number, MapVersion mapVersion)
        {
            var mapFile = Find(files, mapFileName);
            if (mapFile == null) return;

            var map = ResourceMap.Read(mapFile.FullName, number, mapVersion);
            maps.Add(map);

            var volumeFile = Find(files, volumeFileName);
            if (volumeFile != null)
            {
                var volume = ResourceVolume.Read(map, volumeFile.FullName, number);
                volumes.Add(volume);
            }
            else
            {
                throw new Exception("Volume file not found: " + volumeFileName);
            }
        }

        // resource.p01 ... resource.p## are resource.000 split across floppies and merged by the installer.
        // unfortunately resource.000 ... resource.0## was also used for this, as in qfg3.
        static bool ParseExtensionNumber(string fileName, string requiredPrefix, out byte number, bool isSplitVolume)
        {
            number = 0;
            int extensionPos = fileName.LastIndexOf('.');
            if (extensionPos == -1) return false;

            string prefix = fileName.Substring(0, extensionPos);
            string suffix = fileName.Substring(extensionPos + 1);

            if (isSplitVolume)
            {
                if (suffix[0] == 'p' || suffix[0] == 'P' || suffix[0] == '0')
                {
                    suffix = suffix.Substring(1);
                }
                else
                {
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(requiredPrefix) &&
                !requiredPrefix.Equals(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            bool success = byte.TryParse(suffix, out number);
            if (isSplitVolume && number == 0)
            {
                success = false;
            }
            return success;
        }

        CompressionFormat DetectCompressionFormat()
        {
            // SCI0:  0 = None, 1 = LZW0     2 = Huffman
            // SCI1:  0 = None, 1 = Huffman, 2 = LZW1,    3 = ...
            //
            // Huffman is only used for picture resources.
            // No pictures were ever compressed with LZW0.
            // If there are any pictures with compression 1, it's SCI1.
            // If there are any non-pictures with compression 1, it's SCI0.
            // If there are any non-pictures with compression >= 2, it's SCI1.
            //
            // SCI Companion tests the first 10 views to see if any use
            // compression >= 2. But what if those views were uncompressed?
            // (I'm not aware of this failing anywhere in practice, I
            // used the same heuristic originally.)
            foreach (var entry in volumes.SelectMany(v => v.Entries))
            {
                if (ToResourceType(entry.Type) == ResourceType.Pic)
                {
                    if (entry.Compression == 1)
                    {
                        // has to be Huffman because LZW0 was never used for pictures
                        return CompressionFormat.SCI1;
                    }
                }
                else
                {
                    if (entry.Compression == 1)
                    {
                        // has to be LZW0 because Huffman is only for pictures
                        return CompressionFormat.SCI0;
                    }
                    if (entry.Compression >= 2)
                    {
                        // has to be LZW1 or later because Huffman is only for pictures
                        return CompressionFormat.SCI1;
                    }
                }
            }
            return CompressionFormat.SCI0;
        }

        Compression GetCompression(UInt16 compression)
        {
            switch (compression)
            {
                case 0: return Compression.None;
                case 1: return (compressionFormat == CompressionFormat.SCI0) ?
                               Compression.Lzw0 :
                               Compression.Huffman;
                case 2: return (compressionFormat == CompressionFormat.SCI0) ?
                               Compression.Huffman :
                               Compression.Lzw1;
                case 3: return Compression.Lzw1View;
                case 4: return Compression.Lzw1Pic;
                case 8:  // LSCI
                case 18:
                case 19:
                case 20: return Compression.DclImplode;
                case 32: return Compression.StackerLzs;
                default: throw new Exception("Unknown compression: " + compression);
            }
        }

        static ResourceType ToResourceType(byte b)
        {
            return (ResourceType)(b & 0x7f);
        }

        Span MergeFiles(List<FileInfo> files)
        {
            var fileSizes = files.Select(f => (int)f.Length).ToList();
            int totalSize = fileSizes.Sum();
            var buffer = new byte[totalSize];
            int position = 0;
            for (int i = 0; i < files.Count; ++i)
            {
                using (var stream = File.Open(files[i].FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    position += stream.Read(buffer, position, fileSizes[i]);
                }
            }
            return new Span(buffer);
        }
    }

    public abstract class Source
    {
        public abstract Span GetData();
    }

    public class VolumeSource : Source
    {
        public string Name { get; private set; }
        public Compression Compression { get; private set; }
        public Span Packed { get; private set; }
        public Span Unpacked { get; private set; }
        public UInt32 UnpackedSize { get; private set; }

        public VolumeSource(string name, Compression compression, Span packed, UInt32 unpackedSize)
        {
            Name = name;
            this.Packed = packed;
            Compression = compression;
            this.UnpackedSize = unpackedSize;

            if (compression == Compression.None)
            {
                Unpacked = packed; // re-use the span
                this.UnpackedSize = (UInt32)Unpacked.Length; // ignore the incoming param
            }
        }

        public override Span GetData()
        {
            if (Unpacked == null)
            {
                byte[] buffer = Decompressor.Decompress(Compression, Packed, (int)UnpackedSize);
                Unpacked = new Span(buffer);
            }
            return Unpacked;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PatchSource : Source
    {
        public string Name { get; private set; }
        public ResourceId Id { get; private set; }
        public string FileName { get; private set; }
        public Span Span { get; private set; }

        public PatchSource(ResourceId id, string fileName)
        {
            Id = id;
            FileName = fileName;
            Name = Path.GetFileName(fileName);
        }

        public override Span GetData()
        {
            if (Span == null)
            {
                byte[] buffer = File.ReadAllBytes(FileName);

                // Patch files have two byte headers where the first byte has
                // the resource type. flag $80 is set in later versions.
                // The second byte is zero, except in LSCI where it's the length
                // of the original filename that follows it.
                // TSN\Clubhouse 50.SO header: 82 09 tsnmap.sc
                int patchHeaderLength = 2 + buffer[1];
                Span = new Span(buffer, patchHeaderLength); // skip patch header
            }
            return Span;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class MacResourceForkSource : Source
    {
        public string Name { get; private set; }
        public ResourceId Id { get; private set; }
        public Span Packed { get; private set; }
        public Span Unpacked { get; private set; }

        public MacResourceForkSource(string name, ResourceId id, bool canBeCompressed, Span packed)
        {
            Name = name;
            Id = id;
            this.Packed = packed;
            if (!canBeCompressed)
            {
                Unpacked = packed;
            }
        }

        public override Span GetData()
        {
            if (Unpacked == null)
            {
                Unpacked = MacDecompressor.Decompress(Packed);
            }
            return Unpacked;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}