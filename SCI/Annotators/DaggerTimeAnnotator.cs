using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class DaggerTimeAnnotator
    {
        public static void Run(Game game)
        {
            string timeCheckProc = game.GetExport(0, 10);

            // convert all integers passed to triggerAndClock:doit to hex
            // and annotate them with the clock time that is to display.
            // not all calls trigger the clock, some just set must do flags.
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Text == "doit:" &&
                    (node.Parent.At(0).Text == "triggerAndClock" ||
                     (node.Parent.At(0).At(0).Text == "ScriptID" &&
                      node.Parent.At(0).At(1).Text == "22" &&
                      node.Parent.At(0).At(2).Text == "0")))
                {
                    var intNode = node.Next() as Integer;
                    if (intNode == null) continue;

                    // format triggerAndClock:doit(param1) as hex
                    intNode.SetHexFormat();

                    // annotate as clock time if present in the param.
                    // "10:15 pm" for example.
                    string clockTime = FormatClockTime(intNode.Number);
                    intNode.Annotate(clockTime);
                }

                if (node.At(0).Text == timeCheckProc &&
                    node.At(1) is Integer)
                {
                    var intNode = node.At(1) as Integer;

                    intNode.SetHexFormat();

                    // "has 10:00pm occurred?"
                    // "can 10:00pm occur?"
                    bool prereq = (node.At(2) is Integer && node.At(2).Number != 0);
                    string clockTime = FormatClockTime(intNode.Number);
                    string annotation = "";
                    if (clockTime != "")
                    {
                        if (prereq)
                        {
                            annotation = "can " + clockTime + " occur?";
                        }
                        else
                        {
                            annotation = "has " + clockTime + " occurred?";
                        }
                    }
                    intNode.Annotate(annotation);
                }
            }
        }

        static string FormatClockTime(int i)
        {
            // clock time is upper 2 bytes,
            // if those aren't set then no time.
            if (i <= 0xff) return "";

            // highest byte is hour.
            // second highest is quarter hour 0-3: (00, 15, 30, 45)
            // no am/pm, it's the hour to display with clock hand
            int hour = i >> 12;
            int min = (i & 0x0f00) >> 8;
            string clock = hour + ":" + (min * 15).ToString("00");
            if (7 <= hour && hour < 12)
                clock += " pm";
            else
                clock += " am";
            return clock;
        }
    }
}
