using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._10._Façade
{
    internal class _1_Facade
    {
        /// <summary>
        /// The 'Subsystem ClassA' class
        /// </summary>
        public class SubSystemOne
        {
            public void MethodOne()
            {
                Console.WriteLine(" SubSystemOne Method");
            }
        }
        /// <summary>
        /// The 'Subsystem ClassB' class
        /// </summary>
        public class SubSystemTwo
        {
            public void MethodTwo()
            {
                Console.WriteLine(" SubSystemTwo Method");
            }
        }
        /// <summary>
        /// The 'Subsystem ClassC' class
        /// </summary>
        public class SubSystemThree
        {
            public void MethodThree()
            {
                Console.WriteLine(" SubSystemThree Method");
            }
        }
        /// <summary>
        /// The 'Subsystem ClassD' class
        /// </summary>
        public class SubSystemFour
        {
            public void MethodFour()
            {
                Console.WriteLine(" SubSystemFour Method");
            }
        }
        /// <summary>
        /// The 'Facade' class
        /// </summary>
        public class Facade
        {
            SubSystemOne one;
            SubSystemTwo two;
            SubSystemThree three;
            SubSystemFour four;
            public Facade()
            {
                one = new SubSystemOne();
                two = new SubSystemTwo();
                three = new SubSystemThree();
                four = new SubSystemFour();
            }
            public void MethodA()
            {
                Console.WriteLine("\nMethodA() ---- ");
                one.MethodOne();
                two.MethodTwo();
                four.MethodFour();
            }
            public void MethodB()
            {
                Console.WriteLine("\nMethodB() ---- ");
                two.MethodTwo();
                three.MethodThree();
            }
        }
        public static void Drive()
        {
            Facade facade = new Facade();
            facade.MethodA();
            facade.MethodB();
            // Wait for user
            Console.ReadKey();
        }
    }
}
