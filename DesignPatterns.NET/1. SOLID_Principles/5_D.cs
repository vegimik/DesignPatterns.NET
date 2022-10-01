using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.SOLID_Principles
{
    //Depedency Inverse Principle
    internal class _5_D
    {
        internal class Program
        {
            public enum RelationShip
            {
                Parent,
                children,
                Sibling
            }

            public class Person
            {
                public string Name { get; set; }
                public Person(string name)
                {
                    Name = name;
                }
            }

            public interface IRelationshipBrowser
            {
                IEnumerable<Person> FindAllChildrenOf(string name);

            }


            public class Relationships : IRelationshipBrowser
            {
                private List<(Person, RelationShip, Person)> relations = new List<(Person, RelationShip, Person)>();

                public void AddParentandChild(Person parent, Person child)
                {
                    relations.Add((parent, RelationShip.Parent, child));
                    relations.Add((child, RelationShip.children, parent));
                }
                public void AddSiblings(Person person1, Person person2)
                {
                    relations.Add((person1, RelationShip.Sibling, person2));
                }

                public IEnumerable<Person> FindAllChildrenOf(string name)
                {
                    return (IEnumerable<Person>)relations.Where(x => x.Item1.Name == name && x.Item2 == RelationShip.Parent);
                }

                public List<(Person, RelationShip, Person)> Relations => relations;
            }

            //public Program(Relationships relationships)
            //{
            //    var relations = relationships.Relations;

            //    foreach (var item in relations.Where(x => x.Item1.Name == "John" && x.Item2 == RelationShip.Parent))
            //    {
            //        Console.WriteLine($"john has a child called {item.Item3.Name}");

            //    }

            //}

            public Program(IRelationshipBrowser relationshipBrowser)
            {

                foreach (var item in relationshipBrowser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"john has a child called {item.Name}");

                }

            }

            static void Drive()
            {

                var parent = new Person("John");
                var child1 = new Person("Chris");
                var child2 = new Person("Mary");


                var relationships = new Relationships();
                relationships.AddParentandChild(parent, child1);
                relationships.AddParentandChild(parent, child2);


            }
        }
    }
}
