using SCI.Language;
using SCI.Annotators.Visitors;

namespace SCI.Annotators
{
    static class UninitializedTempAnnotator
    {
        public static void Run(Game game, bool annotateMaybes)
        {
            foreach (var function in game.GetFunctions())
            {
                var visitor = new UninitializedTempVisitor(function, annotateMaybes);
                visitor.Visit(function.Code);
            }
        }
    }
}
