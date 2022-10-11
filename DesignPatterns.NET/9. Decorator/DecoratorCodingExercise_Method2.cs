using NUnit.Framework;
using System;

namespace DesignPatterns.NET._9._Decorator
{
    internal class DecoratorCodingExercise_Method2
    {
        public class Bird
        {
            public int Age { get; set; }

            public string Fly()
            {
                return (Age < 10) ? "flying" : "too old";
            }
        }

        public class Lizard
        {
            public int Age { get; set; }

            public string Crawl()
            {
                return (Age > 1) ? "crawling" : "too young";
            }
        }

        public class Dragon // no need for interfaces
        {
            private int age;
            private Bird bird = new Bird();
            private Lizard lizard = new Lizard();

            public int Age
            {
                set { age = bird.Age = lizard.Age = value; }
                get { return age; }
            }

            public string Fly()
            {
                return bird.Fly();
            }

            public string Crawl()
            {
                return lizard.Crawl();
            }
        }


        public static void Drive()
        {
            var dragon = new Dragon();
            dragon.Age = 3;

            Console.WriteLine($"Age: {dragon.Age}");
            Console.WriteLine($"Fly: {dragon.Fly()}");
            Console.WriteLine($"Crawl: {dragon.Crawl()}");

        }


        [TestFixture]
        public class DecoratorCodingExercise_Method2_TestSuit
        {
            [Test]
            public void Test()
            {
                var dragon = new Dragon();

                Assert.That(dragon.Fly(), Is.EqualTo("flying"));
                Assert.That(dragon.Crawl(), Is.EqualTo("too young"));

                dragon.Age = 20;

                Assert.That(dragon.Fly(), Is.EqualTo("too old"));
                Assert.That(dragon.Crawl(), Is.EqualTo("crawling"));
            }
        }
    }
}
