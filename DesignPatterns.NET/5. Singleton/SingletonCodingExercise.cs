using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.NET._5._Singleton.SingletonCodingExercise_Method_2;

/// <summary>
/// Author: Vegim Karaçica
/// </summary>
/// 
namespace DesignPatterns.NET._5._Singleton
{
    [TestFixture]
    public class FirstTestSuite
    {
        [Test]
        public void Test()
        {
            var obj = new object();
            Assert.IsTrue(SingletonTester.IsSingleton(() => obj));
            Assert.IsFalse(SingletonTester.IsSingleton(() => new object()));
        }
    }

    internal class SingletonCodingExercise_Method_2
    {
        public class SingletonTester
        {
            public static bool IsSingleton(Func<object> func)
            {
                var obj1 = func();
                var obj2 = func();
                return ReferenceEquals(obj1, obj2);
            }
        }
        static void Drive()
        {

        }

    }
}
