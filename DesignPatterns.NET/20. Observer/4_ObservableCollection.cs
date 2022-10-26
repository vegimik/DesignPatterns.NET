using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._20._Observer
{
    internal class _4_ObservableCollection
    {
        public class Market : INotifyPropertyChanged
        {
            private float volatility;

            public float Volatility
            {
                get => volatility;
                set
                {
                    if (value.Equals(volatility)) return;
                    OnPropertyChanged();
                }

            }


            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }


        public static void Drive()
        {
            var market = new Market();
            Console.WriteLine($"Object is just created:");
            market.PropertyChanged += (sender, eventArgs) =>
            {
                if (eventArgs.PropertyName == "Volatility")
                {
                    Console.WriteLine($"There has changed to variable: {eventArgs.PropertyName}");
                }
            };

            market.Volatility = 15;
        }
    }
}
