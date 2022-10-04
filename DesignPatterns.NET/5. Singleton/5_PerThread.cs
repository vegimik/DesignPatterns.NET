using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.NET._5._Singleton
{
    internal class _5_PerThread
    {
        public sealed class PerThreadSingleton
        {
            private static ThreadLocal<PerThreadSingleton> threadInstance
              = new ThreadLocal<PerThreadSingleton>(
                () => new PerThreadSingleton());

            public int Id;
            public ThreadState ThreadState { get; private set; }

            private PerThreadSingleton()
            {
                Id = Thread.CurrentThread.ManagedThreadId;
                ThreadState = Thread.CurrentThread.ThreadState;
            }

            public static PerThreadSingleton Instance => threadInstance.Value;
        }


        public static void Drive()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"t1: " + PerThreadSingleton.Instance.Id);
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"t2: " + PerThreadSingleton.Instance.Id + ", ThreadState: " + PerThreadSingleton.Instance.ThreadState);
                Console.WriteLine($"t2 again: " + PerThreadSingleton.Instance.Id);
            });
            Task.WaitAll(t1, t2);
        }


    }
}
