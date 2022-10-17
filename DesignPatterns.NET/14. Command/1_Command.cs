using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._14._Command
{
    public class BankAccount
    {
        private int balance;
        private int overdrraftLimit = -500;
        public void Deposit(int amount)
        {
            this.balance += amount;
            Console.WriteLine($"Deposit ${amount} balance is now {balance}");
        }

        public void WithDraw(int amount)
        {
            if (balance - amount >= overdrraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance is now {balance}");
            }
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}, {nameof(overdrraftLimit)}: {overdrraftLimit}";
        }
    }

    public interface ICommand
    {
        void Call();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        private Action action;
        private int amount;
        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }

        public enum Action
        {
            Deposit, Withdraw
        }
        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    break;
                case Action.Withdraw:
                    account.WithDraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal class _1_Command
    {

        public static void Drive()
        {
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 500),
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 1500),
            };

            Console.WriteLine(ba);

            foreach (var command in commands)
            {
                command.Call();
            }
            Console.WriteLine(ba);

        }
    }
}
