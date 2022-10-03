using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._4._Prototype
{
    internal class _2_CopyconstructorsAndDeepCopy
    {
        public interface IPrototype<T>
        {
            T DeepCOpy();

        }

        public class Person : ICloneable, IPrototype<Person>
        {
            public string[] Names;
            public Address Address;

            public Person(string[] names, Address address)
            {
                Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
                Address = address ?? throw new ArgumentNullException(paramName: address.ToString());
            }

            public Person(Person other)
            {
                Names = other.Names;
                Address = new Address(other.Address);
            }

            /// <summary>
            /// Deep Copy
            /// </summary>
            /// <returns></returns>
            public object Clone()
            {
                return new Person(Names, (Address)Address.Clone());
            }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }

            public Person DeepCOpy()
            {
                return new Person(Names, Address.DeepCOpy());
            }
        }

        public class Address : ICloneable, IPrototype<Address>
        {
            public string StreetName;
            public int HouseNumber;

            public Address(Address otherAddress)
            {
                StreetName = otherAddress.StreetName;
                HouseNumber = otherAddress.HouseNumber;
            }

            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName ?? throw new ArgumentNullException(paramName: nameof(streetName));
                HouseNumber = houseNumber;
            }

            /// <summary>
            /// Deep Copy
            /// </summary>
            /// <returns></returns>
            public object Clone()
            {
                return new Address(StreetName, HouseNumber);
            }

            public Address DeepCOpy()
            {
                return new Address(StreetName, HouseNumber);
            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }

        static void Drive()
        {

            var john = new Person(new[] { "John", "Smith" }, new Address("London", 123));


            var jane = (Person)john.Clone();
            jane.Address.HouseNumber = 456;

            var jane2 = new Person(john);
            jane2.Address.HouseNumber = 789;


            var jane3 = new Person(john);
            jane3.Address.HouseNumber = 789;



            Console.WriteLine(john);
            Console.WriteLine(jane);
            Console.WriteLine(jane2);
            Console.WriteLine(jane3);

        }

    }
}
