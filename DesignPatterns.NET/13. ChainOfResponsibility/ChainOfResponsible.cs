using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._13._ChainOfResponsibility
{
    internal class ChainOfResponsible
    {
        public abstract class Creature
        {
            public int Attack { get; set; }
            public int Defense { get; set; }

        }

        public class Goblin : Creature
        {
            private Creature Creature;
            public Goblin(Game game)
            {
                this.Attack = 1;
                this.Defense = 1;
                //game.Creatures?.ForEach(x =>
                //{
                //    x.Defense += 1;
                //});
                //foreach (var creature in game.Creatures)
                //{
                //    creature.Defense += 1;
                //}
            }

        }

        public class GoblinKing : Goblin
        {
            public GoblinKing(Game game) : base(game)
            {
                this.Attack = 3;
                this.Defense = 3;
                //game.Creatures.ForEach(x =>
                //{
                //    x.Attack += 1;
                //    x.Defense += 1;
                //});
                //foreach (var creature in game.Creatures)
                //{
                //    creature.Attack += 1;
                //    creature.Defense += 1;
                //}

            }
        }

        public class Game : ICollection<Creature>
        {
            public IList<Creature> Creatures;
            public Game()
            {
                this.Creatures = new List<Creature>();
            }

            public int Count => throw new NotImplementedException();

            public bool IsReadOnly => throw new NotImplementedException();

            public void Add(Creature item)
            {
                if (item.GetType() == typeof(GoblinKing))
                {
                    foreach (var creature in Creatures)
                    {
                        creature.Attack += 1;
                        creature.Defense += 1;
                    }
                }
                if (item.GetType() == typeof(Goblin))
                {
                    foreach (var creature in Creatures)
                    {
                        creature.Defense += 1;
                    }
                }
                Creatures.Add(item);
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(Creature item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(Creature[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<Creature> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public bool Remove(Creature item)
            {
                if (!Creatures.Contains(item)) return false;
                if (item.GetType() == typeof(GoblinKing))
                {
                    foreach (var creature in Creatures)
                    {
                        creature.Attack -= 1;
                        creature.Defense -= 1;
                    }
                }
                if (item.GetType() == typeof(Goblin))
                {
                    foreach (var creature in Creatures)
                    {
                        creature.Defense -= 1;
                    }
                }
                Creatures.Remove(item);
                return true;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public static void Drive()
        {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Add(goblin);
            var gobli2 = new Goblin(game);
            game.Add(gobli2);
            var gobli3 = new Goblin(game);
            //Assert.That(goblin.Attack, Is.EqualTo(1));
            //Assert.That(goblin.Defense, Is.EqualTo(1));

        }
    }
}
