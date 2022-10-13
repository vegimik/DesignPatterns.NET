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
            public static List<string> strings = new List<string>();
            private int[] names;

            public string fullName { get; set; }

            public User2(string fullname)
            {
                int getOrAdd(string s)
                {
                    int indx = strings.IndexOf(s);
                    if (indx != -1)
                    {
                        return indx;
                    }
                    else
                    {
                        strings.Add(s);
                        return strings.Count - 1;
                    }
                }
                names = fullname.Split(' ').Select(getOrAdd).ToArray();
            }

            public string FullName => string.Join(" ", names.Select(i => strings[i]));
        }

        public static void Drive()
        {

        }

        [TestFixture]
        public class _1_RepeatingUserNames_TestSuit
        {
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
