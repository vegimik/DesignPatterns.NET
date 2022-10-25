using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.NET._20._Observer._3_ObserverViaSpecialInerface;

namespace DesignPatterns.NET._20._Observer
{
    internal class _3_ObserverViaSpecialInerface : IObserver<Event>
    {
        public class Event
        {

        }

        public class FallsIllEvent : Event
        {
            public string Address;
        }

        public class Person : IObservable<Event>
        {
            private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();

            public IDisposable Subscribe(IObserver<Event> observer)
            {
                var subscription = new Subscription(this, observer);
                subscriptions.Add(subscription);
                return subscription;
            }

            public void FallsIll()
            {
                foreach (var item in subscriptions)
                {
                    item.Observer.OnNext(new FallsIllEvent
                    {
                        Address = "Fushe Kosove"
                    });
                }
            }

            private class Subscription : IDisposable
            {
                private readonly Person person;
                public readonly IObserver<Event> Observer;

                public Subscription(Person person, IObserver<Event> observer)
                {
                    this.person = person;
                    Observer = observer;

                }

                public void Dispose()
                {
                    person.subscriptions.Remove(this);
                }
            }
        }

        public void Drive()
        {
            var person = new Person();
            IDisposable subscription = person.Subscribe(this);

            person.FallsIll();
            person.OfType<FallsIllEvent>()
                .Subscribe(args =>
                {
                    Console.WriteLine($"A doctor is required at {args.Address}");
                });
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent fallsIllEvent)
            {
                Console.WriteLine($"A doctor is required at {fallsIllEvent.Address}");
            }

        }
    }
}
