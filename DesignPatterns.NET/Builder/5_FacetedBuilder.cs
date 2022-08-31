using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Builder
{
    internal class FacetedBuilder
    {
        public class Person
        {
            //Address
            public string StreetAddress, Postcode, City;

            public string CompanyName, Position;
            public int AnnualIncome;

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
            }
        }

        public class PersonBuilder
        {
            protected Person person = new Person();

            public PersonJobBuilder Works => new PersonJobBuilder(person);
            public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

            public static implicit operator Person(PersonBuilder builder)
            {
                return builder.person;
            }

        }


        public class PersonAddressBuilder : PersonBuilder
        {
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostalCode(string postalCode)
            {
                person.Postcode = postalCode;
                return this;
            }
            public PersonAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }
        }



        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public PersonJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }
            public PersonJobBuilder Earning(int amount)
            {
                person.AnnualIncome = amount;
                return this;
            }
        }


        static void Drive()
        {
            var builder = new PersonBuilder();
            Person person = builder
                .Lives
                    .At("Pajazit Islami")
                    .In("Fushe Kosove")
                    .WithPostalCode("EnShpi_Or_EnBunes")
                .Works
                    .At("Engineer")
                    .Earning(123456);

            Console.WriteLine(person);





        }
    }
}
