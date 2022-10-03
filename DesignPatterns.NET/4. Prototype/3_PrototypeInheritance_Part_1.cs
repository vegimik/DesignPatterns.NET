using System;

namespace DesignPatterns.NET._4._Prototype
{
    internal class _3_PrototypeInheritance_Part_1
    {
        public interface IDeepCopyable<T>
            where T : new()
        {
            T DeepCopy();
        }

        public class Address : IDeepCopyable<Address>
        {
            public string StreetName;
            public int HouseNumber;

            public Address()
            {

            }
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

            public Address DeepCopy()
            {
                return (Address)MemberwiseClone();
            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }

        public class Person : IDeepCopyable<Person>
        {
            public string[] Names;
            public Address Address;

            public Person()
            {

            }

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


            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }

            public virtual Person DeepCopy()
            {
                return new Person((string[])Names.Clone(), Address.DeepCopy());
            }
        }

        public class Employee : Person, IDeepCopyable<Employee>
        {
            public int Salary;

            public Employee()
            {

            }
            public Employee(int salary)
            {
                Salary = salary;
            }

            public Employee(string[] names, Address address, int salary) : base(names, address)
            {
                Salary = salary;
            }

            public override string ToString()
            {
                return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
            }

            public override Employee DeepCopy()
            {
                return new Employee((string[])Names.Clone(), Address.DeepCopy(), Salary);
            }
        }


        static void Drive()
        {
            var john = new Employee();
            john.Names = new[] { "John", "Doe" };
            john.Address = new Address
            {
                HouseNumber = 123,
                StreetName = "London Road"
            };
            john.Salary = 321000;

            Employee copyJohn = john.DeepCopy();

            copyJohn.Names[1] = "Smith";
            copyJohn.Address.HouseNumber++;
            copyJohn.Salary = 123000;

            Console.WriteLine(john);
            Console.WriteLine(copyJohn);




        }
    }
}