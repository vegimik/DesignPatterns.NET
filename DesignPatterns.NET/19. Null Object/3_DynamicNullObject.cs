using Autofac;
using ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._19._Null_Object
{
    internal class _3_DynamicNullObject
    {

        public interface ILog
        {
            void Info(string msg);
            void Warn(string msg);
        }

        public class ConsoleLog : ILog
        {
            public void Info(string msg)
            {
                Console.WriteLine(msg);
            }

            public void Warn(string msg)
            {
                Console.WriteLine("WARNING: " + msg);
            }
        }

        public class BankAccount
        {
            private ILog log;
            private int balance;

            public BankAccount(ILog log)
            {
                this.log = log;
            }

            public void Deposit(int amount)
            {
                balance += amount;
                // check for null everywhere
                log?.Info($"Deposited ${amount}, balance is now {balance}");
            }

            public void Withdraw(int amount)
            {
                if (balance >= amount)
                {
                    balance -= amount;
                    log?.Info($"Withdrew ${amount}, we have ${balance} left");
                }
                else
                {
                    log?.Warn($"Could not withdraw ${amount} because balance is only ${balance}");
                }
            }
        }

        public sealed class NullLog : ILog
        {
            public void Info(string msg)
            {

            }

            public void Warn(string msg)
            {

            }
        }

        public class Null<T> : DynamicObject where T : class
        {
            public static T Instance
            {
                get
                {
                    if (!typeof(T).IsInterface)
                        throw new ArgumentException("I must be an interface type");

                    return new Null<T>().ActLike<T>();
                }
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = Activator.CreateInstance(binder.ReturnType);
                return true;
            }

            private class Empty { }
        }

        public static void Drive()
        {
            //var log = new ConsoleLog();
            //ILog log = null;
            //var log = new NullLog();
            var log = Null<ILog>.Instance;
            var ba = new BankAccount(log);
            ba.Deposit(100);
            ba.Withdraw(200);

            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            //cb.RegisterInstance((ILog)null);
            cb.RegisterType<NullLog>().As<ILog>();
            //cb.Register<BankAccount>(ctx => new BankAccount(null));

            using (var c = cb.Build())
            {
                var baResolved = c.Resolve<BankAccount>();


            }
        }


    }
}
