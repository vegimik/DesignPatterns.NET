using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.NET._5._Singleton._2_TestibilityIssue;

namespace DesignPatterns.NET._5._Singleton
{



    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSignletonTest()
        {
            var db = SignletonDAtabase.Instance;
            var db2 = SignletonDAtabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SignletonDAtabase.InstanceCount, Is.EqualTo(1));
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SignletonRecordFInder();
            var names = new[] { "Seoul", "Mexico City" };
            var tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 17400000));
        }

    }

    public class _2_TestibilityIssue
    {
        public static string fileName = @"C:\Users\Twin\OneDrive\Desktop\UdemyCourses\4. Design Patterns C# and .Net\DesignPatterns.NET\DesignPatterns.NET\5. Singleton\capitals.txt";
        public interface IDatabase
        {
            int GetPopulation(string name);
        }

        public class SignletonDAtabase : IDatabase
        {
            private Dictionary<string, int> capitals = new Dictionary<string, int>();
            private static int instanceCount;
            public static int InstanceCount => instanceCount;

            private static Lazy<SignletonDAtabase> instance = new Lazy<SignletonDAtabase>(() => new SignletonDAtabase());
            public static SignletonDAtabase Instance => instance.Value;
            public SignletonDAtabase()
            {
                instanceCount++;
                Console.WriteLine("Initilaizing database");

                capitals = File.ReadAllLines(Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"))
                    .Batch(2)
                    .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1).Trim()))
                    ;
            }

            public int GetPopulation(string name)
            {
                return capitals[name];
            }
        }

        public class SignletonRecordFInder
        {
            public int GetTotalPopulation(IEnumerable<string> names)
            {
                int result = 0;
                foreach (var item in names)
                {
                    result += SignletonDAtabase.Instance.GetPopulation(item);

                }
                return result;
            }
        }


        static void Drive()
        {
            var db = SignletonDAtabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");




        }
    }
}
