using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._1._General
{
    public class Operators
    {
        public readonly struct Digit
        {
            private readonly byte digit;

            public Digit(byte digit)
            {
                if (digit > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(digit), "Digit cannot be greater than nine.");
                }
                this.digit = digit;
            }

            public static implicit operator byte(Digit d) => d.digit;
            public static explicit operator Digit(byte b) => new Digit(b);

            public override string ToString() => $"{digit}";
        }

        static void Main(string[] args)
        {
            var d = new Digit(7);

            byte number = d;
            Console.WriteLine(number);  // output: 7

            Digit digit = (Digit)number;
            Console.WriteLine(digit);  // output: 7

        }
    }
}
