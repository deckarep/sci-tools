using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;
using SCI.Annotators;
using SCI.Decompile;
using SCI.Resource;

// Snuffer is the CLI driver for the SCI script decompiler and annotators.
//
// I regenerate the sci-scripts repository with:
//
// snuffer --mass --decompile --annotate --clean --sco --jobs 3 c:\sierra c:\sci-scripts
//
// Except really:
//
// snuffer -mdaco -j 3 c:\sierra c:\sci-scripts
//
// Decompiling and annotating are separate operations because they are two
// different programs. The annotators were originally developed to improve
// SCI Companion's decompiled scripts, years before my decompiler.

namespace Snuffer
{
    static class Snuffer
    {
        static int Main(string[] args)
        {
            // parse command line
            var parser = new Parser(config => config.HelpWriter = null);
            var result = parser.ParseArguments<Options>(args);
            if (result.Tag == ParserResultType.NotParsed)
            {
                Usage();
                return -1;
            }
            var opt = result.Value;
            if (!Validate(opt)) return -1;

            // the default output directory is the input directory
            opt.OutputDirectory = opt.OutputDirectory ?? opt.InputDirectory;

            int gameSuccessCount = 0;
            int gameCount = 0;
            int scriptSuccessCount = 0;
            int scriptCount = 0;
            int functionSuccessCount = 0;
            int functionCount = 0;
            int loopSuccessCount = 0;
            int loopCount = 0;
            int loopFunctionSuccessCount = 0;
            int loopFunctionCount = 0;
            var timer = new Stopwatch();
            timer.Start();

            var inputDirectory = new DirectoryInfo(opt.InputDirectory);
            if (opt.Mass)
            {
                // Mass Mode
                var localizationMap = ReadLocalizationMap(inputDirectory);
                DirectoryInfo[] gameDirectories = inputDirectory.GetDirectories();
                var parallelOptions = new ParallelOptions();
                parallelOptions.MaxDegreeOfParallelism = opt.Jobs;
                Parallel.ForEach(gameDirectories, parallelOptions, gameDirectory =>
                {
                    var decompiler = new Decompiler();
                    decompiler.CatchExceptions = true;
                    decompiler.CreateGameIni = (opt.Decompile && opt.Sco);
                    decompiler.CreateScoFiles = (opt.Decompile && opt.Sco);
                    decompiler.DumpGraphs = opt.Graph;

                    string outputDirectory = Path.Combine(opt.OutputDirectory, gameDirectory.Name);
                    if (ProcessDirectory(gameDirectory, new DirectoryInfo(outputDirectory), opt, decompiler, localizationMap))
                    {
                        Interlocked.Increment(ref gameSuccessCount);
                    }
                    Interlocked.Increment(ref gameCount);
                    Interlocked.Add(ref scriptSuccessCount, decompiler.ScriptSuccessCount);
                    Interlocked.Add(ref scriptCount, decompiler.ScriptCount);
                    Interlocked.Add(ref functionSuccessCount, decompiler.FunctionSuccessCount);
                    Interlocked.Add(ref functionCount, decompiler.FunctionCount);
                    Interlocked.Add(ref loopSuccessCount, decompiler.SolvedLoopCount);
                    Interlocked.Add(ref loopCount, decompiler.LoopCount);
                    Interlocked.Add(ref loopFunctionSuccessCount, decompiler.SolvedLoopFunctionCount);
                    Interlocked.Add(ref loopFunctionCount, decompiler.LoopFunctionCount);
                });
            }
            else
            {
                // Single Mode
                var decompiler = new Decompiler();
                decompiler.CatchExceptions = true;
                decompiler.CreateGameIni = (opt.Decompile && opt.Sco);
                decompiler.CreateScoFiles = (opt.Decompile && opt.Sco);
                decompiler.DumpGraphs = opt.Graph;
                var localizationMap = ReadLocalizationMap(inputDirectory.Parent);
                if (ProcessDirectory(new DirectoryInfo(opt.InputDirectory), new DirectoryInfo(opt.OutputDirectory), opt, decompiler, localizationMap))
                {
                    Interlocked.Increment(ref gameSuccessCount);
                }
                Interlocked.Increment(ref gameCount);
                Interlocked.Add(ref scriptSuccessCount, decompiler.ScriptSuccessCount);
                Interlocked.Add(ref scriptCount, decompiler.ScriptCount);
                Interlocked.Add(ref functionSuccessCount, decompiler.FunctionSuccessCount);
                Interlocked.Add(ref functionCount, decompiler.FunctionCount);
                Interlocked.Add(ref loopSuccessCount, decompiler.SolvedLoopCount);
                Interlocked.Add(ref loopCount, decompiler.LoopCount);
                Interlocked.Add(ref loopFunctionSuccessCount, decompiler.SolvedLoopFunctionCount);
                Interlocked.Add(ref loopFunctionCount, decompiler.LoopFunctionCount);
            }
            timer.Stop();

            if (opt.Mass)
            {
                Print("GAMES:         ", gameSuccessCount, gameCount, "Processed");
            }
            if (opt.Decompile)
            {
                Print("SCRIPTS:       ", scriptSuccessCount, scriptCount, "Decompiled");
                Print("FUNCTIONS:     ", functionSuccessCount, functionCount, "Decompiled");
                Print("LOOPS:         ", loopSuccessCount, loopCount, "Solved");
                Print("LOOP FUNCTIONS:", loopFunctionSuccessCount, loopFunctionCount, "Solved");
            }
            if (opt.Mass)
            {
                Console.WriteLine("THREADS: " + opt.Jobs);
            }
            Console.WriteLine("TIME: " + ((int)timer.Elapsed.TotalMinutes) + " minutes, " +
                timer.Elapsed.Seconds + "." + timer.Elapsed.Milliseconds / 100  + " seconds");

            return 0;
        }

        static void Print(string first, int success, int total, string second)
        {
            decimal percent = (total > 0) ? ((decimal)(success) / total) * 100.0M : 0;
            Console.WriteLine("{0} {1:N0} / {2:N0} -- {3:0.0000}% {4}",
                first, success, total, percent, second);
        }

        static bool ProcessDirectory(DirectoryInfo inputDirectory, DirectoryInfo outputDirectory, Options opt, Decompiler decompiler, LocalizationMap map)
        {
            if (opt.Clean)
            {
                try
                {
                    if (!opt.Graph)
                    {
                        CleanScripts(outputDirectory);
                    }
                    else
                    {
                        CleanGraphs(outputDirectory);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error cleaning: " + outputDirectory.FullName);
                    Console.WriteLine(ex);
                    return false;
                }
            }

            if (opt.Graph)
            {
                Console.WriteLine("Dumping graphs: " + inputDirectory.Name);
            }
            else if (opt.Decompile)
            {
                Console.WriteLine("Decompiling: " + inputDirectory.Name);
            }
            else if (opt.Annotate)
            {
                Console.WriteLine("Annotating: " + inputDirectory.Name);
            }

            Game game = null;
            if (opt.Graph || opt.Decompile)
            {
                // load the game
                try
                {
                    game = new Game(inputDirectory.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading: " + inputDirectory.FullName);
                    Console.WriteLine(ex);
                    return false;
                }
                if (game.Scripts.Count == 0)
                {
                    Console.WriteLine("Game not found: " + inputDirectory.FullName);
                    return false;
                }

                // decompile (or dump graphs)
                try
                {
                    decompiler.Run(game, outputDirectory.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error decompiling: " + inputDirectory.FullName);
                    Console.WriteLine(ex);
                    return false;
                }
            }

            if (opt.Annotate)
            {
                if (opt.Decompile)
                {
                    Console.WriteLine("Annotating: " + inputDirectory.Name);
                }

                // annotations occur in-place on the input directory, unless we decompiled
                // first, in which case we're annotating the output directory.
                var scriptDirectory = opt.Decompile ? outputDirectory : inputDirectory;

                if (scriptDirectory.GetDirectories("src").Length == 0)
                {
                    Console.WriteLine("No scripts in: " + scriptDirectory.FullName);
                    return false;
                }

                // try to find a message directory from a map
                DirectoryInfo messageDirectory = null;
                if (map != null && map.ContainsKey(inputDirectory.Name) && inputDirectory.Parent != null)
                {
                    messageDirectory = new DirectoryInfo(Path.Combine(inputDirectory.Parent.FullName, map[inputDirectory.Name]));
                    if (!messageDirectory.Exists)
                    {
                        Console.WriteLine("Message directory doesn't exist: " + messageDirectory.FullName);
                        return false;
                    }
                }

                try
                {
                    // if we've already decompiled the game then use that directory as
                    // the resource directory. otherwise, use the input directory,
                    // unless a resource directory was provided on the command line.
                    var resources = game?.ResourceManager;
                    if (resources == null && !string.IsNullOrWhiteSpace(opt.ResourceDirectory))
                    {
                        string resourceDirectory = opt.ResourceDirectory;
                        if (opt.Mass)
                        {
                            resourceDirectory = Path.Combine(resourceDirectory, scriptDirectory.Name);
                        }
                        resources = new ResourceManager(resourceDirectory);
                    }

                    Annotate.Run(
                        scriptDirectory.FullName,
                        resources,
                        (messageDirectory != null) ? new ResourceManager(messageDirectory.FullName) : null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error annotating: " + scriptDirectory.FullName);
                    Console.WriteLine(ex);
                    return false;
                }
            }
            return true;
        }

        static void CleanScripts(DirectoryInfo dir)
        {
            if (!dir.Exists) return;
            Console.WriteLine("Cleaning scripts: " + dir.FullName);
            string gameIni = Path.Combine(dir.FullName, "game.ini");
            if (File.Exists(gameIni))
            {
                File.Delete(gameIni);
            }
            string polyDir = Path.Combine(dir.FullName, "poly"); // a companion dir
            if (Directory.Exists(polyDir) && Directory.GetFileSystemEntries(polyDir).Length == 0)
            {
                Directory.Delete(polyDir);
            }
            var src = new DirectoryInfo(Path.Combine(dir.FullName, "src"));
            if (src.Exists)
            {
                foreach (var file in src.GetFiles())
                {
                    if (file.Extension.Equals(".sc", StringComparison.OrdinalIgnoreCase) ||
                        file.Extension.Equals(".sco", StringComparison.OrdinalIgnoreCase) ||
                        file.Name.Equals("game.sh", StringComparison.OrdinalIgnoreCase) ||
                        file.Name.Equals("Decompiler.ini", StringComparison.OrdinalIgnoreCase))
                    {
                        file.Delete();
                    }
                }
                if (!Directory.EnumerateFileSystemEntries(src.FullName).Any())
                {
                    src.Delete();
                }
            }
        }

        static void CleanGraphs(DirectoryInfo dir)
        {
            if (!dir.Exists) return;
            Console.WriteLine("Cleaning graphs: " + dir.FullName);
            foreach (var graphFile in dir.GetFiles("*.gv"))
            {
                graphFile.Delete();
            }
        }

        static bool Validate(Options opt)
        {
            if (!opt.Decompile &&
               !opt.Annotate &&
               !opt.Graph &&
               !opt.Clean)
            {
                // nothing to do
                Usage();
                return false;
            }

            if (opt.Graph && (opt.Decompile || opt.Annotate))
            {
                Console.WriteLine("Can't dump graphs and decompile or annotate");
                return false;
            }

            if (opt.Graph && string.IsNullOrWhiteSpace(opt.OutputDirectory))
            {
                Console.WriteLine("Can't dump graphs without an output directory");
                return false;
            }

            if (!Directory.Exists(opt.InputDirectory))
            {
                Console.WriteLine("Input directory doesn't exist: " + opt.InputDirectory);
                return false;
            }

            if (opt.Annotate && !opt.Decompile && !string.IsNullOrWhiteSpace(opt.OutputDirectory))
            {
                Console.WriteLine("Output directory isn't used when just annotating");
                return false;
            }

            if (opt.Jobs < 1)
            {
                Console.WriteLine("Invalid threads: " + opt.Jobs);
                return false;
            }

            return true;
        }

        static LocalizationMap ReadLocalizationMap(DirectoryInfo dir)
        {
            var files = dir.GetFiles("localization-map.txt");
            if (files.Length == 1)
            {
                try
                {
                    return new LocalizationMap(files[0].FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to read " + files[0].Name + ": " + ex.Message);
                }
            }
            return null;
        }

        static void Usage()
        {
            Console.WriteLine(
@"usage: snuffer OPTIONS GAME-DIRECTORY [OUTPUT-DIRECTORY]
example: snuffer -daco c:\sierra\kq6

SCI Script Decompiler & Annotator by sluicebox

  -d, --decompile    Decompile scripts into script files
  -a, --annotate     Annotate script files
  -g, --graph        Dump graphviz files

  -o, --sco          Generate .sco files and game.ini when decompiling
  -c, --clean        Clean output directory of generated files
  -m, --mass         Input directory is a directory of games
  -j, --jobs         Number of jobs when mass'ing (amassing?)
  -r, --resource     Resource directory (for annotating script-only directory)"
            );
        }
    }
}
