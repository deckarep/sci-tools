using System;
using System.Collections.Generic;
using SCI.Resource;

namespace SCI.Annotators
{
    // Entry point to annotating a script directory
    public static class Annotate
    {
        // Single directory, assumes there are resources
        public static void Run(string gameDirectory)
        {
            var game = new Language.Game(gameDirectory);
            var id = GameDefinition.Identify(game);
            var resources = new ResourceManager(gameDirectory);
            Run(id, game, resources);
        }

        // Used by command line interface, loads resources if not loaded
        public static void Run(string gameDirectory, ResourceManager resources, ResourceManager messageResources = null)
        {
            // parse scripts from game directory
            var game = new Language.Game(gameDirectory);
            var id = GameDefinition.Identify(game);

            // parse resources from game directory,
            // unless the caller has provided an existing resource manager.
            if (resources == null)
            {
                resources = new ResourceManager(gameDirectory);
            }

            Run(id, game, resources, messageResources);
        }

        // Everything has been parsed and loaded, just annotate
        public static void Run(GameId id, Language.Game game, ResourceManager resources, ResourceManager messageResources = null)
        {
            var map = new Dictionary<GameId, Type>
            {
                // Archive
                // AstroChicken
                { GameId.Brain1, typeof(Brain1Annotator) },
                { GameId.Brain2, typeof(Brain2Annotator) },
                { GameId.Camelot, typeof(CamelotAnnotator) },
                { GameId.Colonel, typeof(ColonelAnnotator) },
                { GameId.Dagger, typeof(DaggerAnnotator) },
                { GameId.Eco1, typeof(Eco1Annotator) },
                { GameId.Eco2, typeof(Eco2Annotator) },
                { GameId.FairyTales, typeof(FairyTalesAnnotator) },
                { GameId.Fpfp, typeof(FpFpAnnotator) },
                { GameId.Gk1, typeof(Gk1Annotator) },
                { GameId.Gk2, typeof(Gk2Annotator) },
                { GameId.GooseEga, typeof(GooseEgaAnnotator) },
                { GameId.Goose256, typeof(Goose256Annotator) },
                { GameId.GooseDeluxe, typeof(GooseDeluxeAnnotator) },
                // Hoyle1-3
                { GameId.Hoyle4, typeof(Hoyle4Annotator) },
                { GameId.Hoyle5, typeof(Hoyle5Annotator) },
                { GameId.Iceman, typeof(IcemanAnnotator) },
                { GameId.Kq1, typeof(Kq1Annotator) },
                { GameId.Kq4, typeof(Kq4Annotator) },
                { GameId.Kq5, typeof(Kq5Annotator) },
                { GameId.Kq6, typeof(Kq6Annotator) },
                { GameId.Kq7, typeof(Kq7Annotator) },
                { GameId.Lighthouse, typeof(LighthouseAnnotator) },
                { GameId.Longbow, typeof(LongbowAnnotator) },
                { GameId.Lsl1, typeof(Lsl1Annotator) },
                { GameId.Lsl2, typeof(Lsl2Annotator) },
                { GameId.Lsl3, typeof(Lsl3Annotator) },
                { GameId.Lsl5, typeof(Lsl5Annotator) },
                { GameId.Lsl6, typeof(Lsl6Annotator) },
                { GameId.Lsl7, typeof(Lsl7Annotator) },
                { GameId.Pepper, typeof(PepperAnnotator) },
                { GameId.Phant1, typeof(Phant1Annotator) },
                { GameId.Phant2, typeof(Phant2Annotator) },
                { GameId.Pq1, typeof(Pq1Annotator) },
                { GameId.Pq2, typeof(Pq2Annotator) },
                { GameId.Pq3, typeof(Pq3Annotator) },
                { GameId.Pq4, typeof(Pq4Annotator) },
                { GameId.PqSwat, typeof(PqSwatAnnotator) },
                { GameId.Qfg1Ega, typeof(Qfg1EgaAnnotator) },
                { GameId.Qfg1Vga, typeof(Qfg1VgaAnnotator) },
                { GameId.Qfg2, typeof(Qfg2Annotator) },
                { GameId.Qfg3, typeof(Qfg3Annotator) },
                { GameId.Qfg4, typeof(Qfg4Annotator) },
                { GameId.Rama, typeof(RamaAnnotator) },
                { GameId.Realm, typeof(RealmAnnotator) },
                { GameId.Shivers, typeof(ShiversAnnotator) },
                // Slater
                { GameId.Sq1, typeof(Sq1Annotator) },
                { GameId.Sq3, typeof(Sq3Annotator) },
                { GameId.Sq4, typeof(Sq4Annotator) },
                { GameId.Sq5, typeof(Sq5Annotator) },
                { GameId.Sq6, typeof(Sq6Annotator) },
                { GameId.Torin, typeof(TorinAnnotator) },
                // Trivia
            };

            // create new GameAnnotator from id
            Type type;
            if (!map.TryGetValue(id, out type)) type = typeof(GameAnnotator);
            var annotator = (GameAnnotator)Activator.CreateInstance(type);

            // configure annotator
            annotator.Game = game;
            annotator.Resources = resources;
            annotator.MessageResources = messageResources ?? resources;

            // run and write
            annotator.Run();
            annotator.Write();
        }
    }
}
