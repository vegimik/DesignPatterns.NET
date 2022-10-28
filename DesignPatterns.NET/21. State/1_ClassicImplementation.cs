using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._21._State
{
    internal class _1_ClassicImplementation
    {
        public class Switch
        {
            public State State = new OffState();
            public void On()
            {
                State.On(this);
            }
            public void Off()
            {
                State.off(this);
            }

        }

        public abstract class State
        {
            public virtual void On(Switch @switch)
            {
                Console.WriteLine("Light is already on.");
            }

            public virtual void off(Switch @switch)
            {
                Console.WriteLine("Light is already off.");
            }
        }
        public class OnState : State
        {
            public OnState()
            {
                Console.WriteLine("Light turned on.");
            }

            public override void off(Switch @switch)
            {
                Console.WriteLine("Turning light off...");
                @switch.State = new OffState();
            }
        }

        public class OffState : State
        {
            public OffState()
            {
                Console.WriteLine("Light turned off.");
            }
            public override void On(Switch @switch)
            {
                Console.WriteLine("Turning light on...");
                @switch.State = new OnState();
            }
        }


        public static void Drive()
        {
            var @switch = new Switch();
            @switch.On();
            @switch.Off();
        }
    }
}
