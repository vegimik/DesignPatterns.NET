using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._19._Null_Object
{
    internal class _2_NullObjectSingleton
    {
        public interface ILog
        {
            public void Warn();

            public static ILog Null => NullLog.Instance;

            private sealed class NullLog : ILog
            {
                private NullLog() { }

                private static Lazy<NullLog> instance =
                  new Lazy<NullLog>(() => new NullLog());

                public static ILog Instance => instance.Value;

                public void Warn()
                {

                }
            }
        }

        public class BankAccount
        {
            public BankAccount(ILog log)
            {

            }
        }

        public static void Drive()
        {
            ILog log = ILog.Null;
        }

    }
}
