using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._20._Observer
{
    internal class _5_BidirectionalObserver
    {
        public class Product : INotifyPropertyChanged
        {
            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    if (name == value) return;
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string proeprtyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprtyName));
            }
            public override string ToString()
            {
                return $"Product: {nameof(Name)}: {Name}";
            }
        }

        public class Window : INotifyPropertyChanged
        {

            private string productName;
            public string ProductName
            {
                get { return productName; }
                set
                {
                    if (productName == value) return;
                    productName = value;
                    OnPropertyChanged(nameof(productName));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string proeprtyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprtyName));
            }

            public override string ToString()
            {
                return $"Window: {nameof(ProductName)}: {ProductName}";
            }
        }

        public sealed class BidirectionalBinding : IDisposable
        {
            private bool disposed;

            public BidirectionalBinding(INotifyPropertyChanged first, Expression<Func<object>> firstProperty,
                INotifyPropertyChanged second, Expression<Func<object>> secondProperty)
            {
                if (firstProperty.Body is MemberExpression firstExpr
                    && secondProperty.Body is MemberExpression secondExpr)
                {
                    if (firstExpr.Member is PropertyInfo firstProp
                        && secondExpr.Member is PropertyInfo secondProp)
                    {
                        first.PropertyChanged += (sender, eventArgs) =>
                        {
                            if (!disposed)
                            {
                                secondProp.SetValue(second, firstProp.GetValue(first));
                            }
                        };
                        second.PropertyChanged += (sender, eventArgs) =>
                        {
                            if (!disposed)
                            {
                                firstProp.SetValue(first, secondProp.GetValue(second));
                            }
                        };
                    }

                }

            }
            public void Dispose()
            {
                disposed = true;
            }
        }


        public static void Drive()
        {
            var product = new Product
            {
                Name = "Book"
            };
            var window = new Window
            {
                ProductName = "Book"
            };

            //product.PropertyChanged += (sender, eventArgs) =>
            //{
            //    if (eventArgs.PropertyName == "Name")
            //    {
            //        Console.WriteLine("Name changed in Product");
            //        window.ProductName = product.Name;
            //    }
            //};

            //window.PropertyChanged += (sender, eventArgs) =>
            //{
            //    if (eventArgs.PropertyName == "ProductName")
            //    {
            //        Console.WriteLine("Name changed in Window");
            //        product.Name = window.ProductName;
            //    }
            //};

            using var binding = new BidirectionalBinding(product,
                () => product.Name,
                window,
                () => window.ProductName);

            window.ProductName = "Smart Book";

            Console.WriteLine($"Name changed in {product.Name}");
            Console.WriteLine($"ProductName changed in {window.ProductName}");
        }
    }
}
