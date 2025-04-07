using System.Linq;
using SCI.Language;

// Game identification, for applying the right annotator.
//
// Identification is based solely on decompiled scripts,
// so it primarily uses the game object name from script 0.
// If that's not enough then it also uses the presence of a
// unique script number and/or object name.

namespace SCI
{
    public enum GameId
    {
        Unknown,

        Archive,
        Brain1,
        Brain2,
        Camelot,
        Colonel,
        Dagger,
        Eco1,
        Eco2,
        FairyTales,
        Fpfp,
        Gk1,
        Gk2,
        GooseEga,    // SCI0
        Goose256,    // SCI1
        GooseDeluxe, // SCI2
        Hoyle1,
        Hoyle2,
        Hoyle3,
        Hoyle4,
        Hoyle5,
        Iceman,
        Jones,
        Kq1,
        Kq4,
        Kq5,
        Kq6,
        Kq7,
        Lighthouse,
        Longbow,
        Lsl1,
        Lsl2,
        Lsl3,
        Lsl5,
        Lsl6,
        Lsl7,
        Pepper,
        Phant1,
        Phant2,
        Pq1,
        Pq2,
        Pq3,
        Pq4,
        PqSwat,
        Qfg1Ega,
        Qfg1Vga,
        Qfg2,
        Qfg3,
        Qfg4,
        Rama,
        Realm,
        Shivers,
        Slater,
        Sq1,
        Sq3,
        Sq4,
        Sq5,
        Sq6,
        Torin,
        Trivia,
    }

    class GameDefinition
    {
        public GameId Id;
        public string GameObjectName;
        public int UniqueScript;
        public string UniqueObject;

        public GameDefinition(GameId id, string gameObjectName, int uniqueScript = -1, string uniqueObject = null)
        {
            Id = id;
            GameObjectName = gameObjectName;
            UniqueScript = uniqueScript;
            UniqueObject = uniqueObject;
        }

        public override string ToString() { return Id.ToString() + " - " + GameObjectName; }

        public static GameDefinition[] Games =
        {
            new GameDefinition(GameId.Archive, "archive"),
            new GameDefinition(GameId.Brain1, "Brain", 380), // room 380
            new GameDefinition(GameId.Brain2, "Brain", 16),  // BrainWindow
            new GameDefinition(GameId.Camelot, "ARTHUR"),
            new GameDefinition(GameId.Colonel, "CB1"),
            new GameDefinition(GameId.Colonel, "MM1"), // demo
            new GameDefinition(GameId.Eco1, "eco"),
            new GameDefinition(GameId.Eco2, "Rain"),
            new GameDefinition(GameId.FairyTales, "Tales"),
            new GameDefinition(GameId.Fpfp, "FP"),
            new GameDefinition(GameId.Gk1, "GK"),
            new GameDefinition(GameId.Gk2, "GK2"),
            new GameDefinition(GameId.Gk2, "GK2Demo"),
            new GameDefinition(GameId.GooseEga, "MG", 0, "globalMGSound"),
            new GameDefinition(GameId.Goose256, "MG", 979), // event handler or Wander
            new GameDefinition(GameId.GooseDeluxe, "MG", 64999),
            new GameDefinition(GameId.Hoyle1, "cardGames"),
            new GameDefinition(GameId.Hoyle2, "solitare"),
            new GameDefinition(GameId.Hoyle3, "hoyle3", 2), // cnick doesn't have 2, does have 12
            new GameDefinition(GameId.Hoyle4, "hoyle4", 999),
            //new GameDefinition(GameId.Hoyle4, null, 100, "crazy8s.opt"), // mac
            new GameDefinition(GameId.Hoyle5, "hoyle4", 64999),
            new GameDefinition(GameId.Iceman, "iceMan"),
            new GameDefinition(GameId.Iceman, "iceDemo"),
            new GameDefinition(GameId.Jones, "jones"),
            new GameDefinition(GameId.Kq1, "kq1"),
            new GameDefinition(GameId.Kq1, "Demo000"),
            new GameDefinition(GameId.Kq4, "KQ4"),
            new GameDefinition(GameId.Kq5, "KQ5"),
            new GameDefinition(GameId.Kq6, "Kq6"),
            new GameDefinition(GameId.Kq7, "KQ7CD"),
            new GameDefinition(GameId.Kq7, "KQ7"), // demo
            new GameDefinition(GameId.Lighthouse, "Lite"),
            new GameDefinition(GameId.Longbow, "Longbow"),
            new GameDefinition(GameId.Longbow, "RH"), // demo
            new GameDefinition(GameId.Lsl1, "LSL1", 803), // cnick doesn't have speed test 803
            // lsl1 demo is "ll1" but i only want generic annotator
            new GameDefinition(GameId.Lsl2, "LSL2"),
            new GameDefinition(GameId.Lsl3, "LSL3"),
            new GameDefinition(GameId.Lsl5, "LSL5"),
            new GameDefinition(GameId.Lsl6, "LSL6"),
            new GameDefinition(GameId.Lsl7, "L7"),
            new GameDefinition(GameId.Pepper, "twisty"),
            new GameDefinition(GameId.Phant1, "Scary"),
            new GameDefinition(GameId.Phant2, "p2"),
            new GameDefinition(GameId.Pq1, "pq1"),
            new GameDefinition(GameId.Pq2, "PQ"),
            new GameDefinition(GameId.Pq3, "pq3"),
            new GameDefinition(GameId.Pq4, "pq4"),
            new GameDefinition(GameId.PqSwat, "Swat"),
            new GameDefinition(GameId.Qfg1Ega, "HQ"), // demo: Demo000, but i want generic annotator
            new GameDefinition(GameId.Qfg1Ega, "Glory", 0, "dirHandler"),
            new GameDefinition(GameId.Qfg1Vga, "Glory", 0, "qg1Messager"),
            new GameDefinition(GameId.Qfg1Vga, "unknown_8", 206, "money_pouch"), // mac
            new GameDefinition(GameId.Qfg2, "Trial"), // demo: HQ2Demo, but i want generic annotator
            new GameDefinition(GameId.Qfg3, "Glory", 0, "qg3DoVerbCode"),
            new GameDefinition(GameId.Qfg4, "Glory", 1, "glryInit"), // handles demo too
            new GameDefinition(GameId.Dagger, "LB2"), // demo: lb2, but i want generic annotator
            new GameDefinition(GameId.Rama, "Rama"),
            new GameDefinition(GameId.Realm, "RoomZero"),
            new GameDefinition(GameId.Shivers, "SHIVERS"),
            new GameDefinition(GameId.Slater, "TheGame"),
            new GameDefinition(GameId.Sq1, "sq1"),
            new GameDefinition(GameId.Sq1, "SQ1Demo"),
            new GameDefinition(GameId.Sq3, "SQ3"),
            new GameDefinition(GameId.Sq4, "sq4", 808), // sq4/msastro have 808, cnick doesn't. cnick does have room 36
            new GameDefinition(GameId.Sq5, "SQ5"),
            new GameDefinition(GameId.Sq6, "SQ6"),
            new GameDefinition(GameId.Torin, "Torin"),
            new GameDefinition(GameId.Trivia, "quizGame"),
        };

        public static GameId Identify(Game game)
        {
            // filter by game object name
            var gameObjectName = game.GetExport(0, 0);
            var games = Games.Where(g => g.GameObjectName == gameObjectName).ToList();

            // filter by unique script
            games.RemoveAll(g => g.UniqueScript != -1 &&
                                 game.GetScript(g.UniqueScript) == null);

            // filter by unique object
            games.RemoveAll(g => g.UniqueObject != null &&
                                 !(game.GetScript(g.UniqueScript)
                                   .Objects
                                   .Any(o => o.Name == g.UniqueObject)));

            if (games.Select(g => g.Id).Distinct().Count() == 1)
            {
                return games[0].Id;
            }
            return GameId.Unknown;
        }
    }
}
