using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._11._Flyweight
{
    internal class _2_TextFormatting
    {
        public class FormattingText
        {
            public string plainText { get; set; }

            private bool[] capitalize;

            public FormattingText(string plainText)
            {
                this.plainText = plainText;
                capitalize = new bool[plainText.Length];
            }

            public void Capitlize(int start, int end)
            {
                for (int i = start; i < end; i++)
                {
                    capitalize[i] = true;
                }

            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < plainText.Length; i++)
                {
                    var c = plainText[i];
                    sb.Append(capitalize[i] ? char.ToUpper(c) : char.ToLower(c));

                }
                return sb.ToString();
            }
        }

        public class BetterFormattedText
        {
            private string plainText;
            private List<TextRange> textRanges = new List<TextRange>();

            public BetterFormattedText(string plainText)
            {
                this.plainText = plainText;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (int i = 0; i < plainText.Length; i++)
                {
                    var c = plainText[i];
                    foreach (var range in textRanges)
                    {
                        if (range.Covers(i) && range.Capilize)
                            c = char.ToUpper(c);
                        sb.Append(c);
                    }

                }
                return ToString();
            }

            public TextRange GetRange(int start, int end)
            {
                var range = new TextRange
                {
                    Start = start,
                    End = end
                };
                textRanges.Add(range);
                return range;
            }


            public class TextRange
            {
                public int Start, End;
                public bool Capilize, Bool, Italic;

                public bool Covers(int position)
                {
                    return position >= Start && position <= End;
                }
            }
        }


        public static void Drive()
        {
            var ft = new FormattingText("This is a brave new world");
            ft.Capitlize(10, 15);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capilize = true;
            Console.WriteLine(bft);


        }
    }
}
