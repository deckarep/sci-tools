using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // annotates each room with its display text from the upper-left of the screen.
    //
    // this only works on rooms that set their text; many rooms don't and just let the
    // previous text stick around. noun and modNum are the two properties to get.
    // modNum is optional; it overrides the room number.

    static class Gk2RoomTitleAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            var roomClasses = game.GetRoomClasses();

            foreach (var script in game.Scripts)
            {
                foreach (var room in script.Instances.Where(i => roomClasses.Contains(i.Super)))
                {
                    // a noun must exist for there to be a title
                    int noun = room.GetIntegerProperty("noun");
                    if (noun < 1) continue;

                    int modNum = room.GetIntegerProperty("modNum");
                    if (modNum <= -1) // if doesn't exist or -1 then use room number
                    {
                        modNum = script.Number;
                    }

                    var title = messageFinder.GetFirstMessage(modNum, modNum, noun, 0, 0);
                    if (title == null) continue;

                    room.Node.Annotate(title.Text.QuoteMessageText());
                }
            }
        }
    }
}
