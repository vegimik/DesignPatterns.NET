using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._21._State
{
    internal class _2_HandmadeStateMachine
    {
        public enum State
        {
            OffHook,
            Connecting,
            Connected,
            OnHold
        }

        public enum Trigger
        {
            CallDialed,
            HangUp,
            CallConnected,
            PlacedOnHold,
            TakeOffHold,
            LeftMessage
        }


        public static Dictionary<State, List<(Trigger, State)>> rules = new Dictionary<State, List<(Trigger, State)>>
        {
            [State.OffHook] = new List<(Trigger, State)>
            {
                (Trigger.CallDialed, State.Connecting)
            },
            [State.Connecting] = new List<(Trigger, State)>
            {
                (Trigger.HangUp, State.OffHook),
                (Trigger.CallConnected, State.Connected),
            },
            [State.Connected] = new List<(Trigger, State)>
            {
                (Trigger.LeftMessage, State.OffHook),
                (Trigger.HangUp, State.OffHook),  (Trigger.PlacedOnHold, State.OnHold),
            },
            [State.OnHold] = new List<(Trigger, State)>
            {
                (Trigger.TakeOffHold, State.Connected),
            },
        };

        public static void Drive()
        {
            var state = State.OffHook;
            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine($"Select a trigger");

                for (int i = 0; i < rules[state].Count; i++)
                {
                    var (t, _) = rules[state][i];
                }
                int input = int.Parse(Console.ReadLine());

                var (_, s) = rules[state][input];
                state = s;
            }



        }


    }
}
