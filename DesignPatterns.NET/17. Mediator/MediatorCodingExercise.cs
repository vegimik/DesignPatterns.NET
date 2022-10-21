using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._17._Mediator
{
    internal class MediatorCodingExercise
    {

        public class Participant
        {
            private readonly Mediator mediator;
            public int Value { get; set; }

            public Participant(Mediator mediator)
            {
                this.mediator = mediator;
                mediator.Alert += Mediator_Alert;
            }

            private void Mediator_Alert(object sender, int e)
            {
                if (sender != this)
                    Value += e;
            }

            public void Say(int n)
            {
                mediator.Broadcast(this, n);
            }
        }

        public class Mediator
        {
            public void Broadcast(object sender, int n)
            {
                Alert?.Invoke(sender, n);
            }

            public event EventHandler<int> Alert;
        }


        public static void Drive()
        {

            Mediator mediator = new Mediator();
            var p1 = new Participant(mediator);
            var p2 = new Participant(mediator);

            Console.WriteLine(p1.Value);
            Console.WriteLine(p2.Value);

            p1.Say(2);

            Console.WriteLine(p1.Value);
            Console.WriteLine(p2.Value);

            p2.Say(4);

            Console.WriteLine(p1.Value);
            Console.WriteLine(p2.Value);



        }
    }
}
