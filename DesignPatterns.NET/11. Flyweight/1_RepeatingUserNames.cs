using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._11._Flyweight
{
    internal class _1_RepeatingUserNames
    {
        public class User
        {
            public string fullName { get; set; }

            public User(string fullname)
            {
                this.fullName = fullName;
            }
        }
        public class User2
        {
            // Fixed: Use Dictionary for O(1) lookups instead of List with O(n) IndexOf
            public static Dictionary<string, int> stringToIndex = new Dictionary<string, int>();
            public static List<string> strings = new List<string>();
            private static readonly object lockObject = new object(); // Fixed: Add thread safety
            private int[] names;

            // Fixed: Remove unused property to avoid confusion
            // public string fullName { get; set; } - This was never used

            public User2(string fullname)
            {
                int getOrAdd(string s)
                {
                    // Fixed: Add thread safety and use Dictionary for better performance
                    lock (lockObject)
                    {
                        if (stringToIndex.TryGetValue(s, out int index))
                        {
                            return index;
                        }
                        else
                        {
                            int newIndex = strings.Count;
                            strings.Add(s);
                            stringToIndex[s] = newIndex;
                            return newIndex;
                        }
                    }
                }
                names = fullname.Split(' ').Select(getOrAdd).ToArray();
            }

            public string FullName => string.Join(" ", names.Select(i => strings[i]));
            
            // Fixed: Add method to clear static collections to prevent memory leaks
            public static void ClearCache()
            {
                lock (lockObject)
                {
                    stringToIndex.Clear();
                    strings.Clear();
                }
            }
        }

        public static void Drive()
        {

        }

        [TestFixture]
        public class _1_RepeatingUserNames_TestSuit
        {
            [SetUp]
            public void Setup()
            {
                // Fixed: Clear cache before each test to prevent memory leaks
                User2.ClearCache();
            }

            [Test]
            public void TestUser()
            {
                var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
                var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());


                var users = new List<User>();
                foreach (var firstName in firstNames)
                {
                    foreach (var lastName in lastNames)
                    {
                        users.Add(new User($"{firstName} {lastName}"));
                    }
                }

                ForceGC();
                dotMemory.Check(memory =>
                {
                    Console.WriteLine(
                        memory.SizeInBytes);
                });

            }

            [Test]
            public void TestUser2()
            {
                var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
                var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());


                var users = new List<User2>();
                foreach (var firstName in firstNames)
                {
                    foreach (var lastName in lastNames)
                    {
                        users.Add(new User2($"{firstName} {lastName}"));
                    }
                }

                ForceGC();
                dotMemory.Check(memory =>
                {
                    Console.WriteLine(
                        memory.SizeInBytes);
                });

            }

            private void ForceGC()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            private string RandomString()
            {
                var random = new Random();
                return new string(Enumerable.Range(0, 10)
                    .Select(x => (char)('a' + random.Next(26)))
                    .ToArray());
            }
        }
    }
}
