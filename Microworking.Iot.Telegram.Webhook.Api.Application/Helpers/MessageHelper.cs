using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Microworking.Iot.Telegram.Webhook.Api.Application.Helpers
{
    public static class MessageHelper
    {
        public static List<string> DivideTerms (string Text)
        {
            List<string> terms = Text.Split(' ').ToList();
            List<string> newTerms = new List<string>();

            terms.ForEach(word => 
            {
                newTerms.Add(WordParser(string.Format("'{0}'", word)));
            });

            return newTerms;
        }

        private static string WordParser(string Text)
        {
            string text = Text.ToUpper();
            StringBuilder newText = new StringBuilder();

            foreach(char letter in text.ToCharArray())
            {
                char newLetter = RemoveAccent(letter);
                newText.Append(newLetter.ToString());
            };

            return newText.ToString();
        }

        private static char RemoveAccent(char Letter)
        {
            List<int> parseA = new List<int> { 131, 132, 133, 134, 142, 143, 160, 181, 183, 192, 198, 199 };
            List<int> parseC = new List<int> { 128, 135 };
            List<int> parseE = new List<int> { 130, 136, 137, 138, 144, 210, 211, 212 };
            List<int> parseI = new List<int> { 139, 140, 141, 161, 173, 213, 214, 215, 216, 222 };
            List<int> parseO = new List<int> { 147, 148, 149, 153, 162, 224, 226, 227, 228, 229 };
            List<int> parseU = new List<int> { 150, 151, 154, 163, 233, 234, 235, 129 };
            List<int> parseN = new List<int> { 164, 165 };
            List<int> parseY = new List<int> { 152, 236, 237 };

            if (parseA.Contains((int)Letter)) return 'A';
            if (parseC.Contains((int)Letter)) return 'C';
            if (parseE.Contains((int)Letter)) return 'E';
            if (parseI.Contains((int)Letter)) return 'I';
            if (parseO.Contains((int)Letter)) return 'O';
            if (parseU.Contains((int)Letter)) return 'U';
            if (parseN.Contains((int)Letter)) return 'N';
            if (parseY.Contains((int)Letter)) return 'Y';

            return Letter;
        }
    }
}