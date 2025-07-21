using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DesignPatterns.NET._5._Singleton
{
    internal class _1_SingletonImplementation
    {
        // Fixed: Use relative path instead of hard-coded absolute path
        public static string fileName = Path.Combine("5. Singleton", "capitals.txt");
        
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

                // Fixed: Add proper error handling for file operations
                try
                {
                    if (File.Exists(fileName))
                    {
                        capitals = File.ReadAllLines(fileName)
                            .Batch(2)
                            .ToDictionary(
                            list => list.ElementAt(0).Trim(),
                            list => int.Parse(list.ElementAt(1).Trim()));
                    }
                    else
                    {
                        Console.WriteLine($"Warning: File {fileName} not found. Using empty database.");
                        capitals = new Dictionary<string, int>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading database: {ex.Message}");
                    capitals = new Dictionary<string, int>();
                }
            }

            public int GetPopulation(string name)
            {
                // Fixed: Add null check and proper error handling
                if (capitals.TryGetValue(name, out int population))
                {
                    return population;
                }
                throw new KeyNotFoundException($"City '{name}' not found in database.");
            }
        }

        public static void Drive()
        {
            var db = SignletonDAtabase.Instance;
            var city = "Tokyo";
            
            // Fixed: Add proper error handling for GetPopulation call
            try
            {
                Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
