using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._11._Flyweight
{
    internal class FlyweightCodingExercise
    {
        public class Sentence
        {
            private List<string> strings = new List<string>();
            private WordToken[] wordTokens;
            public Sentence(string plainText)
            {
                strings = plainText.Split(new char[] { ' ' }, StringSplitOptions.None).ToList();
                wordTokens = new WordToken[strings.Count];
            }

            public WordToken this[int index]
            {
                get
                {
                    wordTokens[index] = wordTokens[index] ?? new WordToken();
                    return wordTokens[index];
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < strings.Count; i++)
                {
                    var c = strings[i];
                    if (wordTokens[i] != null && wordTokens[i].Capitalize)
                        c = c.ToUpper();
                    if (i != strings.Count - 1)
                        c += " ";
                    sb.Append($"{c}");

                }
                return sb.ToString();
            }

            public class WordToken
            {
                public bool Capitalize;
            }
        }
        
        public static void Drive()
        {

            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Console.WriteLine(sentence); // writes "hello WORLD"


        }
    }
}
