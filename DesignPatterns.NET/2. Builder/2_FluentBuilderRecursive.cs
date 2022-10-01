using System;

namespace DesignPatterns.NET.Builder
{
    internal class FluentBuilderRecursive
    {
        public class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }

            public class Builder : PersonJobBuilder<Builder>
            {
            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        public abstract class PersonBuilder
        {
            public Person person { get; set; } = new Person();

            public Person Build()
            {
                return person;
            }

        }

        public class PersonInfoBuilder<T> : PersonBuilder where T : PersonInfoBuilder<T>
        {
            public T Called(string name)
            {
                person.Name = name;
                return (T)this;
            }
        }

        public class PersonJobBuilder<T> : PersonInfoBuilder<PersonJobBuilder<T>> where T : PersonJobBuilder<T>
        {
            public T WorksAsA(string position)
            {
                person.Position = position;
                return (T)this;
            }

        }


        static void Drive()
        {
            var builder = Person.New
                .Called("Gima")
                .WorksAsA("Software Engineer")
                .Build();

            Console.WriteLine(builder);



        }
    }
}
