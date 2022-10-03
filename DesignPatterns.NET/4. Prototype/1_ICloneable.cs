using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._4._Prototype
{
    internal class _1_ICloneable
    {
        public class Person : ICloneable
        {
            public string[] Names;
            public Address Address;

            public Person(string[] names, Address address)
            {
                Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
                Address = address ?? throw new ArgumentNullException(paramName: address.ToString());
            }

            public object Clone()
            {
                return new Person(Names, (Address)Address.Clone());
            }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }
        }

        public class Address : ICloneable
        {
            public string StreetName;
            public int HouseNumber;

            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName ?? throw new ArgumentNullException(paramName: nameof(streetName));
                HouseNumber = houseNumber;
            }

            public object Clone()
            {
                return new Address(StreetName, HouseNumber);
            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }

        public static void Drive()
        {

            var john = new Person(new[] { "John", "Smith" }, new Address("London", 123));


            var jane = (Person)john.Clone();
            jane.Address.HouseNumber = 321;



            Console.WriteLine(john);
            Console.WriteLine(jane);

        }

    }

}
