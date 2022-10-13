using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._12._Proxy
{
    internal class ProxyCodingExercise
    {
        public interface IDrinker
        {
            public bool Drink();
        }
        public interface IDriver
        {
            public bool Drive();
        }
        public interface IDrinkAndDrive : IDrinker, IDriver
        {
            public string DrinkAndDrive();
        }

        public class Person
        {
            public int Age { get; set; }
            public Person()
            {

            }
            public Person(int age)
            {
                this.Age = age;
            }

            public string Drink()
            {
                return "drinking";
            }

            public string Drive()
            {
                return "driving";
            }

            public string DrinkAndDrive()
            {
                return "driving while drunk";
            }
        }

        public class Drinker : IDrinker
        {
            public int Age { get; set; }
            public Drinker(int age)
            {
                Age = age;
            }

            public bool Drink()
            {
                if (Age >= 18) return true;
                return false;
            }
        }

        public class Driver : IDriver
        {
            public int Age { get; set; }
            public Driver(int age)
            {
                Age = age;
            }

            public bool Drive()
            {
                if (Age >= 16) return true;
                return false;
            }
        }

        public class ResponsiblePerson //: IDrinkAndDrive//thats the proxy
        {
            private Person person;
            private Drinker drinker;
            private Driver driver;

            public ResponsiblePerson(Person person)
            {
                this.person = person;
                this.drinker = new Drinker(person.Age);
                this.driver = new Driver(person.Age);
            }

            public int Age
            {
                get => person.Age;
                set => person.Age = value;
            }

            public string Drink()
            {
                if (drinker.Drink())
                {
                    return person.Drink();
                }
                return "too young";
            }

            public string DrinkAndDrive()
            {
                if (!drinker.Drink() && !driver.Drive())
                    return "dead";
                return person.DrinkAndDrive();
            }

            public string Drive()
            {
                if (driver.Drive())
                {
                    return person.Drive();
                }
                return "too young";
            }
        }

        public static void Drive()
        {

            var r = new ResponsiblePerson(new Person(17));

            Console.WriteLine(r.Drink());
            Console.WriteLine(r.Drive());
            Console.WriteLine(r.DrinkAndDrive());


        }

    }
}
