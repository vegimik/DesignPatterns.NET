using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///*
///Composite Proxy: SoA/AoS
namespace DesignPatterns.NET._12._Proxy
{
    internal class _4_CompositeProxy
    {
        public class Creatures
        {
            private readonly int size;
            private byte[] age;
            private int[] x, y;
            public Creatures(int size)
            {
                this.size = size;
                age = new byte[size];
                x = new int[size];
                y = new int[size];
            }

            public struct CreatureProxy
            {
                private readonly Creatures creatures;
                private readonly int index;

                public CreatureProxy(Creatures creatures, int index)
                {
                    this.creatures = creatures;
                    this.index = index;

                }

                public ref byte Age => ref creatures.age[index];
                public ref int X => ref creatures.x[index];
                public ref int Y => ref creatures.y[index];
            }

            public IEnumerator<CreatureProxy> GetEnumerator()
            {
                for (int pos = 0; pos < size; pos++)
                {
                    yield return new CreatureProxy(this, pos);
                }
            }
        }

        public class Creature
        {
            public byte Age;
            public int X, Y;
        }

        public static void Drive()
        {
            //AoS - Array of structure
            var creature = new Creature[100];

            foreach (var item in creature)
            {
                item.X++;
            }

            //AoS/SoA duality
            var creature2 = new Creatures(100); // SoA
            foreach (var item in creature2)
            {
                item.X++;

            }

        }


    }
}
