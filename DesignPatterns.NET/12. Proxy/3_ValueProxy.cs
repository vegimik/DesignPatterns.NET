using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._12._Proxy
{
    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage : IEquatable<Percentage>
    {
        public readonly float value;
        public Percentage(float value)
        {
            this.value = value;
        }
        public static float operator *(float f, Percentage p)
        {
            return f * p.value;
        }
        public static Percentage operator +(Percentage left, Percentage right)
        {
            return new Percentage(left.value + right.value);
        }

        public override string ToString()
        {
            return $"{value*100}%";
        }

        public bool Equals(Percentage other)
        {
            throw new NotImplementedException();
        }
    }

    public static class PercentageExtension
    {
        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100.0f);
        }
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.0f);
        }
    }
    internal class _3_ValueProxy
    {
        public static void Drive()
        {
            Console.WriteLine(
                10f * 5.Percent());

            Console.WriteLine(
                2.Percent() + 3.Percent());

        }
    }
}
