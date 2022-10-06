using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Author: Vegim Karaçica
/// </summary>
/// 
namespace DesignPatterns.NET._5._Singleton
{
    internal class SingletonCodingExercise
    {
        public class SingletonTester
        {
            static Lazy<SingletonTester> instance = new Lazy<SingletonTester>(() => new SingletonTester());
            public static SingletonTester Instance => instance.Value;
            private static List<Func<object>> objectCount;
            public static List<Func<object>> InstanceCount => objectCount;

            public SingletonTester()
            {
                InstanceCount.Add(objDef);

            }

            public SingletonTester(Func<object> func)
            {
                InstanceCount.Add(func);

            }

            public static bool IsSingleton(Func<object> func)
            {
                return InstanceCount.Any(x => x == func) && InstanceCount.Count == 1;
            }

            public Func<object> objDef = () => new object { };
        }

        static void Drive()
        {

        }

    }
}
