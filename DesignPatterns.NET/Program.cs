﻿using Autofac;
using Autofac.Core;
using Autofac.Features.Metadata;
using DesignPatterns.NET._14._Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action = DesignPatterns.NET.Program.Command.Action;

namespace DesignPatterns.NET
{

    public class Program
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




        public static void Main(string[] args)
        {
            var account = new Account(0);
            var cmd = new Command(Command.Action.Deposit, 1000);
            account.Process(cmd);

        }



    }
}
