using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._9._Decorator
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if (Age > 10)
            {
                Console.WriteLine("I am flying");
            }
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
            {
                Console.WriteLine("I am crawling");
            }
        }
    }

    public class Organism
    {

    }

    public class Dragon : Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }


    //  Inheritance
    //  Smart dragon (Dragon)
    //  Extension method
    //  C#8 default interface methods


    internal class _4_MultipleInheritanceWithDefaultInterfacemembers
    {
        public static void Drive()
        {
            Dragon d = new Dragon
            {
                Age = 5
            };

            if (d is ILizard lizard)
            {
                lizard.Crawl();
            }

        }

    }
}
