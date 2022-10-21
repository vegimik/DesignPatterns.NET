﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._18._Memento
{
    internal class _1_Memento
    {
        public class Memento
        {
            public int Balance { get; }

            public Memento(int balance)
            {
                Balance = balance;
            }
        }

        public class BankAccount
        {
            private int balance;

            public BankAccount(int balance)
            {
                this.balance = balance;
            }

            public Memento Deposit(int amount)
            {
                balance += amount;
                return new Memento(balance);
            }

            public void Restore(Memento m)
            {
                balance = m.Balance;
            }

            public override string ToString()
            {
                return $"{nameof(balance)}: {balance}";
            }
        }


        public static void Drive()
        {

            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            Console.WriteLine(ba);

            // restore to m1
            ba.Restore(m1);
            Console.WriteLine(ba);

            ba.Restore(m2);
            Console.WriteLine(ba);
        }
    }
}
