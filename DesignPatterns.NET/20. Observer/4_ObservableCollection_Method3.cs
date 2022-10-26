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
    internal class _4_ObservableCollection_Method3

    {
        public class Market
        {

            public BindingList<float> prices = new BindingList<float>();

            public void AddPrice(float price)
            {
                prices.Add(price);

            }

        }


        public static void Drive()
        {
            var market = new Market();
            Console.WriteLine($"Object is just created:");
            market.prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>)sender)[eventArgs.NewIndex];
                    Console.WriteLine($"Binding list got a price of : {price}");
                }
            };

            market.AddPrice(123);


        }
    }
}
