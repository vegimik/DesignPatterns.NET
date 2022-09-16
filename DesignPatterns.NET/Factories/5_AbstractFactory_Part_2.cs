using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Factories
{
    internal class AbstractFactoryPart2
    {
        public interface IHotDrink
        {
            void Consume();

        }

        internal class Tea : IHotDrink
        {
            public void Consume()
            {
                Console.WriteLine("This tea is nice but I'd prefer it with milk.");
            }
        }
        internal class Coffee : IHotDrink
        {
            public void Consume()
            {
                Console.WriteLine("This coffee is sensational!");
            }
        }


        public interface IHotDrinkFactory
        {
            IHotDrink Prepare(int amount);
        }

        internal class TeaFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.WriteLine($"Put in a tea bag, buil water, pour {amount} ml, add leamon, enjoy");
                return new Tea();
            }
        }

        internal class CoffeeFactory : IHotDrinkFactory
        {
            public IHotDrink Prepare(int amount)
            {
                Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar.");
                return new Coffee();
            }
        }

        public class HotDrinkMachine
        {
            //public enum AvailableDrink
            //{
            //    Tea,
            //    Coffee
            //}

            //public IDictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();
            //public HotDrinkMachine()
            //{
            //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            //    {
            //        ///**   ITS IMPORTANT
            //        /// Thats a good way to create an object of class only by routing through: {nameSpace}+{nameClass}
            //        var factory = (IHotDrinkFactory)
            //            Activator.CreateInstance(
            //                Type.GetType("DesignPatterns.NET" + Enum.GetName(typeof(AvailableDrink), drink)
            //                + "Factory")
            //                );
            //        factories.Add(drink, factory);
            //    }
            //}

            //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
            //{
            //    return factories[drink].Prepare(amount);
            //}
            private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();
            public HotDrinkMachine()
            {
                foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
                {
                    if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                    {
                        factories.Add(
                            Tuple.Create(
                                t.Name.Replace("Factory", string.Empty),
                                (IHotDrinkFactory)Activator.CreateInstance(t)
                            ));
                    }


                }

            }

            public IHotDrink MakeDrink()
            {
                Console.WriteLine("Available Drink!");
                for (int i = 0; i < factories.Count; i++)
                {
                    var tuple = factories[i];
                    Console.WriteLine($"{i}: {tuple.Item1}");
                }

                while (true)
                {
                    string s;
                    if ((s = Console.ReadLine()) != null
                        && int.TryParse(s, out int i)
                        && i >= 0
                        && i < factories.Count)
                    {
                        Console.WriteLine("Specify Amount: ");
                        s = Console.ReadLine();
                        if (s!=null
                            && int.TryParse(s, out int amount)
                            && amount>0)
                        {
                            return factories[i].Item2.Prepare(amount);
                        }


                    }

                }
                Console.WriteLine("Incorrect input, try again!");
            }
        }



        public static void Drive()
        {

            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();


        }
    }
}
