using SCI.Language;

namespace SCI.Annotators
{
    // (Message msgGET modNum noun verb cond seq ...

    static class MessageGetAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            foreach (var script in game.Scripts)
            {
                foreach (var node in script.Root)
                {
                    if (node.At(0).Text == "Message" &&
                        (node.At(1).Text == "msgGET" ||
                         node.At(1).Text == "msgSIZE" ||
                         node.At(1).Text == "msgREF_NOUN" ||
                         node.At(1).Text == "msgREF_VERB" ||
                         node.At(1).Text == "msgREF_COND") &&
                        node.At(2) is Integer &&
                        node.At(3) is Integer &&
                        node.At(4) is Integer &&
                        node.At(5) is Integer &&
                        node.At(6) is Integer)
                    {
                        var message = messageFinder.GetFirstMessage(
                            node.At(2).Number,
                            node.At(2).Number,
                            node.At(3).Number,
                            node.At(4).Number,
                            node.At(5).Number,
                            node.At(6).Number);

                        if (message != null)
                        {
                            node.At(0).Annotate(message.Text.QuoteMessageText());
                        }
                    }
                    else if (node.At(0).Text == "GetMessage" &&
                             node.At(1) is Integer &&
                             node.At(2) is Integer &&
                             node.At(3) is Integer)
                    {
                        // kGetMessage is in eco1 1.0 floppy,
                        // later replaced by kMessage.
                        var message = messageFinder.GetFirstMessage(
                            node.At(1).Number,
                            node.At(1).Number,
                            node.At(2).Number,
                            node.At(3).Number,
                            0); // cond

                        if (message != null)
                        {
                            node.At(0).Annotate(message.Text.QuoteMessageText());
                        }
                    }
                }
            }
        }
    }
}
