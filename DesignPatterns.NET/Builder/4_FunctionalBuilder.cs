using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Builder
{

    public sealed class Person
    {
        public string Name, Position;

    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        //public TSelf Called(string name)
        //=> Do(p => p.Name = name);

        public TSelf Do(Action<Person> action)
        {
            return AddAction(action);
        }

        public Person Build()
        {
            return actions.Aggregate(new Person(), (p, f) => f(p));
        }

        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf)this;
        }

    }

    public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
        => this.Do(p => p.Name = name);

    }

    //public sealed class PersonBuilder
    //{
    //    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    //    public PersonBuilder Called(string name)
    //    => Do(p => p.Name = name);
    //    public PersonBuilder Do(Action<Person> action)
    //    {
    //        return AddAction(action);
    //    }

    //    public Person Build()
    //    {
    //        return actions.Aggregate(new Person(), (p, f) => f(p));
    //    }

    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        actions.Add(p =>
    //        {
    //            action(p);
    //            return p;
    //        });
    //        return this;
    //    }

    //}

    public static class PersonBuilderExtension
    {
        public static PersonBuilder WorksAsA(this PersonBuilder builder, string position) => builder.Do(p => p.Position = position);

    }

    internal class FunctionalBuilder
    {

        static void Drive()
        {

            var person = new PersonBuilder()
                .Called("Vegim")
                .WorksAsA("Software Engineer")
                .Build();



        }
    }
}
