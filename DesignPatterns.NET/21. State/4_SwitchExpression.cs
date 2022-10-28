using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._21._State
{
    internal class _4_SwitchExpression
    {
        public enum Chest
        {
            Open,
            Closed,
            Locked
        }

        public enum Action
        {
            Open,
            Close
        }

        public static Chest Manipulate
            (Chest chest, Action action, bool haveKey) =>
            (chest, action, haveKey) switch
            {
                (Chest.Locked, Action.Open, true) => Chest.Open,
                (Chest.Closed, Action.Open, _) => Chest.Open,
                (Chest.Open, Action.Close, true) => Chest.Locked,
                (Chest.Open, Action.Close, false) => Chest.Closed,
                _=> chest
            };


        public static void Drive()
        {
            var chest = Chest.Locked;
            Console.WriteLine($"Cheat is {chest}");

            chest = Manipulate(chest, Action.Open, true);
            Console.WriteLine($"Cheat is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Cheat is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Cheat is {chest}");

        }
    }
}
