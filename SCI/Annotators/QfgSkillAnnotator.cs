using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // generic QFG skill annotator

    class QfgSkillAnnotator
    {
        public static void Run(Game game,
                               int skillsGlobal, // gEgoStat global number
                               string[] skills,  // array of skills
                               string[] procNames = null) // optional array of global procs that take a skill
        {
            string skillsGlobalName = game.GetGlobal(skillsGlobal).Name;

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                // [gEgoSkill 13]
                if (node is Array &&
                    node.Children.Count == 2 && // prob not necessary
                    node.At(0).Text == skillsGlobalName &&
                    node.At(1) is Integer)
                {
                    AnnotateAttribute(node.At(1), skills);
                }

                // attributeSelector: attribute
                else if ((node.Text == "trySkill:" || // qfg3+
                          node.Text == "useSkill:" || // qfg3+
                          node.Text == "learn:" ||
                          node.Text == "knows:") &&   // qfg1-2
                         node.Next() is Integer)
                {
                    AnnotateAttribute(node.Next(), skills);
                }

                // (TrySkill 10) - qfg1 & qfg2
                else if (procNames != null &&
                         node is List &&
                         procNames.Contains(node.At(0).Text) &&
                         node.At(1) is Integer)
                {
                    AnnotateAttribute(node.At(1), skills);
                }
            }
        }

        static void AnnotateAttribute(Node node, string[] skills)
        {
            string skill = (node.Number < skills.Length) ? skills[node.Number] : "???";
            node.Annotate(skill.SanitizeMessageText());
        }
    }
}
