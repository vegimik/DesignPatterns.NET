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
    internal class _4_ObservableCollection_Method2
    {
        public class Market
        {

            private List<float> prices = new List<float>();

            public void AddPrice(float price)
            {
                prices.Add(price);
                PricesAdded?.Invoke(this, price);

            }

            public event EventHandler<float> PricesAdded;

        }


        public static void Drive()
        {
            var market = new Market();
            Console.WriteLine($"Object is just created:");
            market.PricesAdded += (sender, eventArgs) =>
            {
                Console.WriteLine($"There has changed to variable: {eventArgs}");
            };

            market.AddPrice(123);


        }
    }
}
