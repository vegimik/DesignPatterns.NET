using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._16._Iterator
{
    internal class _3_ArrayBackedProperties
    {
        public class Creature : IEnumerable<int>
        {
            private int[] stats = new int[3];
            private const int strength = 0;
            public int Strength
            {
                get => stats[strength];
                set => stats[strength] = value;
            }
            public int Agility
            {
                get => stats[1];
                set => stats[1] = value;
            }
            public int Intelligence
            {
                get => stats[2];
                set => stats[2] = value;
            }

            public double AverageStat
           => stats.Average();

            public IEnumerator<int> GetEnumerator()
            {
                return stats.AsEnumerable().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int this[int index]
            {
                get => stats[index];
                set => stats[index] = value;
            }
        }

        public static void Drive()
        {
            var creature = new Creature
            {
                Agility = 2,
                Intelligence = 3,
                Strength = 2
            };

            var stat = creature.GetEnumerator();
            Console.WriteLine(stat);

            var statAvg = creature.AverageStat;
            Console.WriteLine(statAvg);
        }
    }
}
