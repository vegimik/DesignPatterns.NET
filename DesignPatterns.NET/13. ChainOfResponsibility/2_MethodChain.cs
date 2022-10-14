using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._13._ChainOfResponsibility
{
    internal class _2_MethodChain
    {
        public class Creature
        {
            public string Name;
            public int Attack, Defense;

            public Creature(string name, int attack, int defense)
            {
                Name = name;
                Attack = attack;
                Defense = defense;
            }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
            }
        }

        public class CretureModifier
        {
            protected Creature creature;
            protected CretureModifier next; // chain field connector

            public CretureModifier(Creature creature)
            {
                this.creature = creature;
            }

            public void Add(CretureModifier other)
            {
                if (next != null) next.Add(other);
                else next = other;
            }

            public virtual void Handle() => next?.Handle();
        }

        public class DoubleAttacModifier : CretureModifier
        {
            public DoubleAttacModifier(Creature creature) : base(creature)
            {
            }

            public override void Handle()
            {
                Console.WriteLine($"Doubling {creature.Name}'s attack");
                creature.Attack *= 2;
                base.Handle();
            }
        }

        public class IncreasedDefenseModifier : CretureModifier
        {
            public IncreasedDefenseModifier(Creature creature) : base(creature)
            {
            }

            public override void Handle()
            {
                Console.WriteLine($"Increasing {creature.Name}'s defense");
                creature.Defense += 3;
                base.Handle();
            }
        }

        public class NoBonusModifier : CretureModifier
        {
            public NoBonusModifier(Creature creature) : base(creature)
            {
            }

            public override void Handle()
            {
                //
            }
        }




        public static void Drive()
        {
            var goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);

            var root = new CretureModifier(goblin);

            root.Add(new NoBonusModifier(goblin));

            Console.WriteLine("Lets double the goblins attack");
            root.Add(new DoubleAttacModifier(goblin));

            Console.WriteLine("Lets increase goblins defense");
            root.Add(new DoubleAttacModifier(goblin));

            root.Handle();

            Console.WriteLine(goblin);
        }
    }
}
