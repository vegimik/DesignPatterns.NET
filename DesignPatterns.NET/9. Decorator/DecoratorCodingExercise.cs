using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._9._Decorator
{
    internal class DecoratorCodingExercise
    {
        public interface IBird
        {
            public int Age { get; set; }
            public string Fly();

        }

        public class Bird : IBird
        {
            public int Age { get; set; }
            public Bird(int age)
            {
                Age = age;
            }

            public string Fly()
            {
                return (Age < 10) ? "flying" : "too old";
            }
        }

        public interface ILizard
        {
            public int Age { get; set; }
            public string Crawl();
        }

        public class Lizard : ILizard
        {
            public int Age { get; set; }
            public Lizard(int age)
            {
                Age = age;
            }

            public string Crawl()
            {
                return (Age > 1) ? "crawling" : "too young";
            }
        }

        public abstract class OrganismDecorator : IBird, ILizard
        {
            private IBird bird;
            private ILizard lizard;
            public virtual int Age { get; set; }
            public OrganismDecorator(int age)
            {
                Age = age;
            }

            public virtual string Crawl()
            {
                return (new Lizard(Age)).Crawl();
            }

            public virtual string Fly()
            {
                return (new Bird(Age)).Fly();
            }
        }




        public class Dragon : OrganismDecorator // no need for interfaces
        {
            public Dragon(int age) : base(age)
            {
            }


            public string Fly()
            {
                return base.Fly();
            }

            public string Crawl()
            {
                return base.Crawl();
            }
        }


        public static void Drive()
        {
            var dragon = new Dragon(3);

            Console.WriteLine($"Age: {dragon.Age}");
            Console.WriteLine($"Fly: {dragon.Fly()}");
            Console.WriteLine($"Crawl: {dragon.Crawl()}");

        }

    }
}
