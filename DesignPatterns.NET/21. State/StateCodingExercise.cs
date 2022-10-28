using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._21._State
{
    internal class StateCodingExercise
    {
        public enum State
        {
            Locked,
            OPEN,
            ERROR
        }
        public class CombinationLock
        {
            private readonly int[] combination;

            public CombinationLock(int[] combination)
            {
                this.combination = combination;
                Reset();
            }

            private void Reset()
            {
                Status = State.Locked.ToString();
                digitsEntered = 0;
                failed = false;
            }

            public string Status;

            private int digitsEntered = 0;
            private bool failed = false;

            public void EnterDigit(int digit)
            {
                if (Status.Equals(State.Locked.ToString())) Status = string.Empty;
                Status += digit.ToString();
                if (combination[digitsEntered] != digit)
                {
                    failed = true;
                }
                digitsEntered++;

                if (digitsEntered == combination.Length)
                    Status = failed ? State.ERROR.ToString() : State.OPEN.ToString();
            }
        }

        public static void Drive()
        {
            var cl = new CombinationLock(new[] { 1, 2, 3, 4, 5 });
            Console.WriteLine("LOCKED");
            cl.EnterDigit(1);
            Console.WriteLine("1");
            cl.EnterDigit(2);
            Console.WriteLine("12");
            cl.EnterDigit(3);
            Console.WriteLine("123");
            cl.EnterDigit(4);
            Console.WriteLine("1234");
            cl.EnterDigit(5);
            Console.WriteLine("OPEN");



            cl = new CombinationLock(new[] { 1, 2, 3 });
            Console.WriteLine("LOCKED");
            cl.EnterDigit(1);
            Assert.That(cl.Status, Is.EqualTo("1"));
            cl.EnterDigit(2);
            Console.WriteLine("12");
            cl.EnterDigit(5);
            Console.WriteLine("ERROR");

        }


        [TestFixture]
        public class StateCodingExercise_TestSuits
        {
            [Test]
            public void TestSuccess()
            {
                var cl = new CombinationLock(new[] { 1, 2, 3, 4, 5 });
                Assert.That(cl.Status, Is.EqualTo("LOCKED"));
                cl.EnterDigit(1);
                Assert.That(cl.Status, Is.EqualTo("1"));
                cl.EnterDigit(2);
                Assert.That(cl.Status, Is.EqualTo("12"));
                cl.EnterDigit(3);
                Assert.That(cl.Status, Is.EqualTo("123"));
                cl.EnterDigit(4);
                Assert.That(cl.Status, Is.EqualTo("1234"));
                cl.EnterDigit(5);
                Assert.That(cl.Status, Is.EqualTo("OPEN"));
            }

            [Test]
            public void TestFailure()
            {
                var cl = new CombinationLock(new[] { 1, 2, 3 });
                Assert.That(cl.Status, Is.EqualTo("LOCKED"));
                cl.EnterDigit(1);
                Assert.That(cl.Status, Is.EqualTo("1"));
                cl.EnterDigit(2);
                Assert.That(cl.Status, Is.EqualTo("12"));
                cl.EnterDigit(5);
                Assert.That(cl.Status, Is.EqualTo("ERROR"));
            }
        }

    }
}
