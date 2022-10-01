using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.SOLID_Principles
{
    //Open-Closed Princliple
    internal class _2_O
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Yuge
        }

        public class Product
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }

            public Product(string name, Color color, Size size)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }
                Name = name;
                Color = color;
                Size = size;
            }
        }

        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach (var item in products)
                {
                    if (item.Size == size)
                        yield return item;
                }
            }
            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (var item in products)
                {
                    if (item.Color == color)
                        yield return item;
                }
            }
            public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
            {
                foreach (var item in products)
                {
                    if (item.Color == color && item.Size == size)
                        yield return item;
                }
            }
        }

        public interface ISpecification<T>
        {
            bool isSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            Color color;
            public ColorSpecification(Color color)
            {
                this.color = color;
            }

            public bool isSatisfied(Product t)
            {
                return t.Color == color;
            }
        }
        public class SizeSpecification : ISpecification<Product>
        {
            Size size;
            public SizeSpecification(Size size)
            {
                this.size = size;
            }

            public bool isSatisfied(Product t)
            {
                return t.Size == size;
            }

        }
        public class AndSpecification<T> : ISpecification<T>
        {
            ISpecification<T> firstSpec, secondSpec;

            public AndSpecification(ISpecification<T> firstSpec, ISpecification<T> secondSpec)
            {
                this.firstSpec = firstSpec ?? throw new ArgumentNullException(paramName: nameof(firstSpec));
                this.secondSpec = secondSpec ?? throw new ArgumentNullException(paramName: nameof(secondSpec));
            }

            public bool isSatisfied(T t)
            {
                return firstSpec.isSatisfied(t) && secondSpec.isSatisfied(t);
            }
        }

        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var item in items)
                {
                    if (spec.isSatisfied(item))
                    {
                        yield return item;
                    }

                }
            }
        }


        static void Drive()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            var pf = new ProductFilter();
            Console.WriteLine("Green products (old):");
            foreach (var item in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {item.Name} is green product");
            }

            var bf = new BetterFilter();
            Console.WriteLine("Green products (old):");
            foreach (var item in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {item.Name} is green product");
            }
            foreach (var item in bf.Filter(products, new SizeSpecification(Size.Large)))
            {
                Console.WriteLine($" - {item.Name} is large product");
            }
            foreach (var item in bf.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($" - {item.Name} is green&large product");
            }

        }
    }
}
