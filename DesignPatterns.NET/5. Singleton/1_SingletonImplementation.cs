using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DesignPatterns.NET._5._Singleton
{
    internal class _1_SingletonImplementation
    {
        public static string fileName = @"C:\Users\Twin\OneDrive\Desktop\UdemyCourses\4. Design Patterns C# and .Net\DesignPatterns.NET\DesignPatterns.NET\5. Singleton\capitals.txt";
        public interface IDatabase
        {
            int GetPopulation(string name);
        }

        public class SignletonDAtabase : IDatabase
        {
            private Dictionary<string, int> capitals = new Dictionary<string, int>();

            private static Lazy<SignletonDAtabase> instance = new Lazy<SignletonDAtabase>(() => new SignletonDAtabase());
            public static SignletonDAtabase Instance => instance.Value;


            public SignletonDAtabase()
            {
                Console.WriteLine("Initilaizing database");

                capitals = File.ReadAllLines(fileName)
                    .Batch(2)
                    .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1).Trim()));
            }

            public int GetPopulation(string name)
            {
                return capitals[name];
            }
        }

        public static void Drive()
        {
            var db = SignletonDAtabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");



        }
    }
}
