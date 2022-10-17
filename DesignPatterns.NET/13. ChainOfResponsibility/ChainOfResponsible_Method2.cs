using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._13._ChainOfResponsibility
{
    internal class ChainOfResponsible_Method2
    {
        public abstract class Creature
        {
            protected Game game;
            protected readonly int baseAttack;
            protected readonly int baseDefense;

            protected Creature(Game game, int baseAttack, int baseDefense)
            {
                this.game = game;
                this.baseAttack = baseAttack;
                this.baseDefense = baseDefense;
            }

            public virtual int Attack { get; set; }
            public virtual int Defense { get; set; }
            public abstract void Query(object source, StatQuery sq);
        }

        public class Goblin : Creature
        {
            public override void Query(object source, StatQuery sq)
            {
                if (ReferenceEquals(source, this))
                {
                    switch (sq.Statistic)
                    {
                        case Statistic.Attack:
                            sq.Result += baseAttack;
                            break;
                        case Statistic.Defense:
                            sq.Result += baseDefense;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    if (sq.Statistic == Statistic.Defense)
                    {
                        sq.Result++;
                    }
                }
            }

            public override int Defense
            {
                get
                {
                    var q = new StatQuery { Statistic = Statistic.Defense };
                    foreach (var c in game.Creatures)
                        c.Query(this, q);
                    return q.Result;
                }
            }

            public override int Attack
            {
                get
                {
                    var q = new StatQuery { Statistic = Statistic.Attack };
                    foreach (var c in game.Creatures)
                        c.Query(this, q);
                    return q.Result;
                }
            }

            public Goblin(Game game) : base(game, 1, 1)
            {
#if (DEBUG)               
                // When you debug you will see thats look like you are calling the override method, very excited.
                var defense = Defense;
#endif


            }

            protected Goblin(Game game, int baseAttack, int baseDefense) : base(game,
              baseAttack, baseDefense)
            {
#if (DEBUG)
                // When you debug you will see thats look like you are calling the override method, very excited.
                var defense = Defense;
                var attack = Attack;
#endif

            }
        }

        public class GoblinKing : Goblin
        {
            public GoblinKing(Game game) : base(game, 3, 3)
            {
            }

            public override void Query(object source, StatQuery sq)
            {
                if (!ReferenceEquals(source, this) && sq.Statistic == Statistic.Attack)
                {
                    sq.Result++; // every goblin gets +1 attack
                }
                else base.Query(source, sq);
            }
        }

        public enum Statistic
        {
            Attack,
            Defense
        }

        public class StatQuery
        {
            public Statistic Statistic;
            public int Result;
        }

        public class Game
        {
            public IList<Creature> Creatures = new List<Creature>();
        }


        public static void Drive()
        {

            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);


            var goblin2 = new Goblin(game);
            game.Creatures.Add(goblin2);


            var goblin3 = new GoblinKing(game);
            game.Creatures.Add(goblin3);


        }
    }
}
