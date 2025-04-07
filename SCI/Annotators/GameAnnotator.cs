using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SCI.Language;

// Generic game annotator. Subclassed by specific game annotators.

namespace SCI.Annotators
{
    public class GameAnnotator
    {
        public Game Game;
        public Resource.ResourceManager Resources;
        public Resource.ResourceManager MessageResources;

        Dictionary<int, List<TextMessage>> textMessages;
        Dictionary<int, List<Message>> messages;
        MessageFinder messageFinder;
        TextMessageFinder textMessageFinder;
        string[] selectors;
        bool? sci32;

        public virtual void Run()
        {
            RunEarly();
            RunLate();
        }

        protected void RunEarly()
        {
            // Class_255_0 => DItem, etc.
            // Do this first to help OriginalSymbolRenamer.
            ClassNamer.Run(Game);

            // original symbols from system scripts
            OriginalSymbolRenamer.Run(Game, Original.SystemSymbols.Headers);

            // original symbols from game scripts
            if (GameHeaders != null)
            {
                OriginalSymbolRenamer.Run(Game, GameHeaders);
            }

            var systemGlobals = SystemGlobalRenamer.Run(Game, Sci32);
            GlobalNameDeducer.Run(Game, systemGlobals);
            // overrides original symbols with my names, that's intentional
            CommonParameterRenamer.Run(Game);
            ScriptIDAnnotator.Run(Game);
            RegionAnnotator.Run(Game);
            RoomAnnotator.Run(Game); // prereq: RoomFileRenamer
            UnusedAnnotator.Run(Game);
            UninitializedTempAnnotator.Run(Game, false); // no more annotating maybes, only for experiments
            KernelCallAnnotator.Run(Game, Sci32);
            EventAnnotator.Run(Game, Sci32);
            PropertyHexer.Run(Game);
            PolygonFormatter.Run(Game);
            EdgeAnnotator.Run(Game);

            if (!Sci32) // optimization; kDisplay doesn't exist in SCI32
            {
                DisplayAnnotator.Run(Game);
            }

            // remove second language from dual language games.
            // optimization: skip this if there are message resources,
            // this only applies to earlier games.
            if (!Messages.Any())
            {
                SecondLanguageRemover.Run(Game, TextMessages);
            }

            // Print {text tuple", etc
            // superstition: no sci32
            if (!Sci32 && TextMessages.Any())
            {
                PrintTextAnnotator.Run(Game, TextMessageFinder, PrintTextFunctions);
            }

            if (Messages.Any())
            {
                // translate PrintTextDef representation of SayProcs (only in by brain2)
                // into a simple dictionary for SayAnnotator. this juggling is because at
                // this point, we haven't renamed the proc yet, so a script/export ID is best.
                Dictionary<string, int> sayProcs = null;
                if (SayProcs != null)
                {
                    sayProcs = new Dictionary<string, int>();
                    foreach (var sp in SayProcs)
                    {
                        string procName = Game.GetExport(sp.ScriptNumber, sp.ExportNumber);
                        sayProcs.Add(procName, sp.ParamIndex);
                    }
                }
                SayAnnotator.Run(Game, MessageFinder, sayProcs);
                MessageGetAnnotator.Run(Game, MessageFinder);

                if (Sci32) // optimization
                {
                    DoAudioMessageAnnotator.Run(Game, MessageFinder);
                }
            }
        }

        protected void RunLate()
        {
            // after ExportRenamer
            LoadAnnotator.Run(Game, Sci32);

            // after ExportRenamer, for "PrintDC"
            // optimization: only read selectors if we can use them
            if (PrintSelectorAnnotator.IsEligible(Game))
            {
                PrintSelectorAnnotator.Run(Game, Selectors);
            }
        }

        public virtual void Write(string scriptDirectory = null)
        {
            TreeWriter.Write(Game, scriptDirectory);
            UpdateScriptObjectFiles();
        }

        protected Dictionary<int, List<TextMessage>> TextMessages
        {
            get
            {
                if (textMessages == null)
                {
                    textMessages = TextMessageReader.ReadMessages(MessageResources);
                }
                return textMessages;
            }
        }

        protected Dictionary<int, List<Message>> Messages
        {
            get
            {
                if (messages == null)
                {
                    // HACK: sq3-mac and hoyle2-mac have resources of type 0x8f (message)
                    // which are of course not message resources, so version detection throws.
                    // i don't know what they are, neither does scummvm.
                    // until i update MessageReader to detect and reject these, whatever they are,
                    // i'm just skipping attempting to parse messages from these two text games.
                    string gameObjectName = Game.GetExport(0, 0);
                    if (gameObjectName != "SQ3" && gameObjectName != "solitare")
                    {
                        messages = MessageReader.ReadMessages(MessageResources, Sci32);
                    }
                    else
                    {
                        messages = new Dictionary<int, List<Message>>();
                    }
                }
                return messages;
            }
        }

        // HACK: i re-used PrintTextDef for SayProcs. sorry, it's useful!
        protected virtual IReadOnlyCollection<PrintTextDef> SayProcs { get { return null; } }

        protected TextMessageFinder TextMessageFinder
        {
            get
            {
                if (textMessageFinder == null)
                {
                    textMessageFinder = new TextMessageFinder(Game.Name, TextMessages);
                }
                return textMessageFinder;
            }
        }

        protected MessageFinder MessageFinder
        {
            get
            {
                if (messageFinder == null)
                {
                    messageFinder = new MessageFinder(Game.Name, Messages);
                }
                return messageFinder;
            }
        }

        protected string[] Selectors
        {
            get
            {
                if (selectors == null)
                {
                    var vocab997 = Resources.GetResource(Resource.ResourceType.Vocab, 997);
                    if (vocab997 != null)
                    {
                        selectors = Resource.SelectorVocab.Read(vocab997);
                        if (IsSci0Early)
                        {
                            selectors = Resource.SelectorVocab.CreateSci0EarlyTable(selectors);
                        }
                    }
                    else
                    {
                        selectors = new string[0];
                    }
                }
                return selectors;
            }
        }

        protected virtual bool IsSci0Early { get { return false; } }

        protected bool Sci32
        {
            get
            {
                if (sci32 == null)
                {
                    sci32 = Game.Scripts.Any(s => s.Number >= 60000);
                }
                return sci32.Value;
            }
        }

        protected virtual IReadOnlyCollection<PrintTextDef> PrintTextFunctions { get { return null; } }

        protected virtual Dictionary<int, Original.Script[]> GameHeaders { get { return null; } }

        void UpdateScriptObjectFiles()
        {
            var scoFileInfos = new DirectoryInfo(Game.ScriptDirectory).GetFiles("*.sco");
            foreach (var script in Game.Scripts)
            {
                // skip if sco file doesn't exist, using the list in memory
                var scoFileName = Path.GetFileNameWithoutExtension(script.FileName) + ".sco";
                if (!scoFileInfos.Any(f => f.Name.Equals(scoFileName, StringComparison.OrdinalIgnoreCase))) continue;

                // read the file
                scoFileName = Path.Combine(Game.ScriptDirectory, scoFileName);
                var sco = ScriptObjectFile.Read(scoFileName);

                // update local names (only names could have changed).
                // remember that SCO has an entry for every "raw" local, with
                // empty strings for those that are subsequent array elements.
                bool update = false;
                foreach (var local in script.Locals.Values)
                {
                    if (local.Number < sco.Locals.Count) // should always be true
                    {
                        if (sco.Locals[local.Number] != local.Name)
                        {
                            sco.Locals[local.Number] = local.Name;
                            update = true;
                        }
                    }
                }

                // update procedure names (only names could have changed)
                foreach (var scoExport in sco.Exports)
                {
                    string newName;
                    if (script.Exports.TryGetValue(scoExport.Number, out newName))
                    {
                        if (scoExport.Name != newName)
                        {
                            scoExport.Name = newName;
                            update = true;
                        }
                    }
                }

                // don't update class names; annotators don't change those

                if (update)
                {
                    byte[] scoBytes = sco.Generate();
                    File.WriteAllBytes(scoFileName, scoBytes);
                }
            }
        }

        protected static Dictionary<int, T> ArrayToDictionary<T>(int startIndex, T[] array)
        {
            var dictionary = new Dictionary<int, T>(array.Length);
            foreach (var element in array)
            {
                dictionary.Add(startIndex + dictionary.Count, element);
            }
            return dictionary;
        }
    }
}
