using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._20._Observer
{
    internal class _1_ObserverEvent
    {
        public class FallsIllEventArgs
        {
            public string Address;
        }
        public class Person : IDisposable
        {
            public event EventHandler<FallsIllEventArgs> FallsIll;

            public void CatchAGold()
            {
                FallsIll?.Invoke(this, new FallsIllEventArgs
                {
                    Address = "Fushe Kosove"
                });
            }

            public void Dispose()
            {
                FallsIll = null;
            }
        }


        public static void Drive()
        {
            var person = new Person();
            person.FallsIll += CallDoctor;

            person.CatchAGold();

        }

        private static void CallDoctor(object sender, FallsIllEventArgs fallsIllEventArgs)
        {
            Console.WriteLine($"A doctor has been called to {fallsIllEventArgs.Address}");
        }
    }
}
