using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Factories
{
    internal class FactoryCodingExercise
    {

        public interface IPerson
        {
            int Id { get; set; }
            string Name { get; set; }
        }

        public class Person : IPerson
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Person()
            {

            }
            public Person(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public override string ToString()
            {
                return $"Person {Id} has name: {Name}";
            }
        }


        public interface IPersonFactory
        {
            public IPerson CreatePerson(string personName);
        }

        public class PersonFactory : IPersonFactory
        {
            public List<IPerson> factories = new List<IPerson>();
            public IPerson CreatePerson(string personName)
            {
                var person = new Person(factories.Count, personName);
                factories.Add(person);
                return person;
            }

            public static implicit operator List<IPerson>(PersonFactory personFactory)
            {
                return personFactory.factories;
            }
        }



        static void Drive()
        {

            var builder = new PersonFactory();
            builder.CreatePerson("Vegim");
            builder.CreatePerson("Vullnet");

            foreach (var item in builder.factories)
                Console.WriteLine(item.ToString());

        }
    }
}
