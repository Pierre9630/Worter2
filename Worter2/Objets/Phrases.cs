using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Worter2
{
    class Phrases
    {
        public List<string> Phrase;
        public List<string> English { get; set; }
        public List<string> Deutsch { get; set; }
        public List<string> Francais { get; set; }

        public string [] WordFinder(List<string> Phrase)
        {
            this.Phrase = Phrase;
            string[] word1 = null;
            foreach(var word in Phrase)
            {
                word1 = word.Split(new char[] { '.', ' ' });
            }
            return word1;
        }

        public string [] ReconnizeWord(string [] Words, string [] WordsToCompare)
        {
            foreach(var Word in Words)
            {

            }

            return null;
        }
    }
}
