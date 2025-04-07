using CommandLine;

namespace Snuffer
{
    class Options
    {
        [Option('d', "decompile")]
        public bool Decompile { get; set; }

        [Option('a', "annotate")]
        public bool Annotate { get; set; }

        [Option('g', "graph")]
        public bool Graph { get; set; }

        [Option('m', "mass")]
        public bool Mass { get; set; }

        [Option('c', "clean")]
        public bool Clean { get; set; }

        [Option('o', "sco")]
        public bool Sco { get; set; }

        [Option('j', "jobs", Default = 1)]
        public int Jobs { get; set; }

        [Value(index: 0, MetaName = "input", Required = true)]
        public string InputDirectory { get; set; }

        [Value(index: 1, MetaName = "output", Required = false)]
        public string OutputDirectory { get; set; }

        [Option('r', "resource")]
        public string ResourceDirectory { get; set; }
    }
}
