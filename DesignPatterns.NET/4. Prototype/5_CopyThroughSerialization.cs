using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPatterns.NET._4._Prototype
{

    public static class ExtensionMethod
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(stream, self);
                stream.Position = 0;
                return (T)s.Deserialize(stream);
            }


        }
    }

    public class _5_CopyThroughSerialization
    {

        [Serializable]
        public class Person
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

        }

        [Serializable]

        public class Address
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

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }


        static void Drive()
        {

            var john = new Person(new[] { "John", "Smith" }, new Address("London", 123));


            //var jane = john.DeepCopy();
            var jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 456;




            Console.WriteLine(john);
            Console.WriteLine(jane);

        }


    }
}
