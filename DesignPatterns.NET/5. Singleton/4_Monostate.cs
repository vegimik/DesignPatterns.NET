using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._5._Singleton
{
    internal class _4_Monostate
    {
        public class ChiefExecutiveOfficer
        {
            private static string name;
            private static int age;

            public string Name
            {
                get => name;
                set => name = value;
            }

            public int Age
            {
                get => age;
                set => age = value;
            }


            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
            }
        }


        static void Drive()
        {
            var ceo = new ChiefExecutiveOfficer();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new ChiefExecutiveOfficer();
            Console.WriteLine(ceo2);
        }
    }
}
