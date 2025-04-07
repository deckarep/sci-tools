using System;

// Default Log behavior is to write to the Console on Error, Warn, and Info.
// Set Log.Level to None to silence all output. Set to All for more.
// Call Log.SetLogger() to do your own thing.

namespace SCI
{
    public enum LogLevel
    {
        None = 0,
        Error,
        Warn,
        Info,
        Debug,
        All,
    }

    public static class Log
    {
        public static LogLevel Level = LogLevel.Info;
        static ILogger logger = new ConsoleLogger();

        public static void SetLogger(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            Log.logger = logger;
        }

        // Error

        public static void Error(string s, Exception ex = null)
        {
            if (Level < LogLevel.Error) return;

            logger.Log(LogLevel.Error, s, ex);
        }

        public static void Error(Resource.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Error) return;

            logger.Log(LogLevel.Error, Format(game, s), ex);
        }

        public static void Error(Resource.Function function, string s, Exception ex = null)
        {
            if (Level < LogLevel.Error) return;

            logger.Log(LogLevel.Error, Format(function, s), ex);
        }

        public static void Error(Language.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Error) return;

            logger.Log(LogLevel.Error, Format(game, s), ex);
        }

        public static void Error(string game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Error) return;

            logger.Log(LogLevel.Error, Format(game, s), ex);
        }

        // Warn

        public static void Warn(string s, Exception ex = null)
        {
            if (Level < LogLevel.Warn) return;

            logger.Log(LogLevel.Warn, s, ex);
        }

        public static void Warn(Resource.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Warn) return;

            logger.Log(LogLevel.Warn, Format(game, s), ex);
        }

        public static void Warn(Resource.Function function, string s, Exception ex = null)
        {
            if (Level < LogLevel.Warn) return;

            logger.Log(LogLevel.Warn, Format(function, s), ex);
        }

        public static void Warn(Language.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Warn) return;

            logger.Log(LogLevel.Warn, Format(game, s), ex);
        }

        public static void Warn(string game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Warn) return;

            logger.Log(LogLevel.Warn, Format(game, s), ex);
        }

        // Info

        public static void Info(string s, Exception ex = null)
        {
            if (Level < LogLevel.Info) return;

            logger.Log(LogLevel.Info, s, ex);
        }

        public static void Info(Resource.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Info) return;

            logger.Log(LogLevel.Info, Format(game, s), ex);
        }

        public static void Info(Resource.Function function, string s, Exception ex = null)
        {
            if (Level < LogLevel.Info) return;

            logger.Log(LogLevel.Info, Format(function, s), ex);
        }

        public static void Info(Language.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Info) return;

            logger.Log(LogLevel.Info, Format(game, s), ex);
        }

        public static void Info(string game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Info) return;

            logger.Log(LogLevel.Info, Format(game, s), ex);
        }

        // Debug

        public static void Debug(string s, Exception ex = null)
        {
            if (Level < LogLevel.Debug) return;

            logger.Log(LogLevel.Debug, s, ex);
        }

        public static void Debug(Resource.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Debug) return;

            logger.Log(LogLevel.Debug, Format(game, s), ex);
        }

        public static void Debug(Resource.Function function, string s, Exception ex = null)
        {
            if (Level < LogLevel.Debug) return;

            logger.Log(LogLevel.Debug, Format(function, s), ex);
        }

        public static void Debug(Language.Game game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Debug) return;

            logger.Log(LogLevel.Debug, Format(game, s), ex);
        }

        public static void Debug(string game, string s, Exception ex = null)
        {
            if (Level < LogLevel.Debug) return;

            logger.Log(LogLevel.Debug, Format(game, s), ex);
        }

        // Format

        static string Format(Resource.Game game, string s)
        {
            return Format(game.ToString(), s);
        }

        static string Format(Resource.Function function, string s)
        {
            var script = function.Script;
            var game = script.Game;

            return string.Format("[{0}] {1} {2} -- {3}",
                game, script, function.FullName, s);
        }

        static string Format(Language.Game game, string s)
        {
            return Format(game.Name, s);
        }

        static string Format(string game, string s)
        {
            return string.Format("[{0}] -- {1}", game, s);
        }
    }

    public interface ILogger
    {
        void Log(LogLevel level, string s, Exception ex);
    }

    class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string s, Exception ex)
        {
            Console.WriteLine(s);
            if (ex != null)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
