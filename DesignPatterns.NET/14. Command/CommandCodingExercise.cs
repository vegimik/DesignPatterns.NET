using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._14._Command
{
    internal class CommandCodingExercise
    {
        public class Command
        {
            public enum Action
            {
                Deposit,
                Withdraw
            }

            public Action TheAction;
            public int Amount;
            public bool Success;
            public Command()
            {

            }
            public Command(Action theAction, int amount)
            {
                TheAction = theAction;
                Amount = amount;
            }
        }

        public class Account
        {
            public int Balance { get; set; }
            public Account()
            {

            }
            public Account(int balance)
            {
                Balance = balance;
            }

            public void Process(Command c)
            {
                switch (c.TheAction)
                {
                    case Command.Action.Deposit:
                        this.Balance += c.Amount;
                        c.Success = true;
                        break;
                    case Command.Action.Withdraw:
                        if (Balance - c.Amount < 0)
                            c.Success = false;
                        else
                        {
                            this.Balance -= c.Amount;
                            c.Success = true;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
        }




        public static void Drive()
        {
            var account = new Account(0);
            var cmd = new Command(Command.Action.Deposit, 1000);
            account.Process(cmd);

        }


        [TestFixture]
        public class CommandCodingExercise_Suit
        {
            [Test]
            public void Test()
            {
                var a = new Account();

                var command = new Command { Amount = 100, TheAction = Command.Action.Deposit };
                a.Process(command);

                Assert.That(a.Balance, Is.EqualTo(100));
                Assert.IsTrue(command.Success);

                command = new Command { Amount = 50, TheAction = Command.Action.Withdraw };
                a.Process(command);

                Assert.That(a.Balance, Is.EqualTo(50));
                Assert.IsTrue(command.Success);

                command = new Command { Amount = 150, TheAction = Command.Action.Withdraw };
                a.Process(command);

                Assert.That(a.Balance, Is.EqualTo(50));
                Assert.IsFalse(command.Success);

            }
        }

    }
}
