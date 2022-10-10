using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._9._Decorator
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float radius;
        public Circle(float radius)
        {
            this.radius = radius;
        }

        public void Resize(float factor)
        {
            this.radius *= factor;
        }

        public string AsString()
        {
            return $"A Circle with radius {radius}";
        }
    }

    public class Sqaure : IShape
    {
        private float side;
        public Sqaure(float side)
        {
            this.side = side;
        }

        public void Resize(float factor)
        {
            this.side *= factor;
        }

        public string AsString()
        {
            return $"A Sqaure with side {side}";
        }
    }

    public class ColoredShape : IShape
    {
        private IShape shape;
        private string color;

        public ColoredShape(IShape shape, string color)
        {
            shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape : IShape
    {
        private IShape shape;
        private float transperancy;

        public TransparentShape(IShape shape, float transperancy)
        {
            this.shape = shape;
            this.transperancy = transperancy;
        }

        public string AsString()
        {
            return $"{shape.AsString()} has {transperancy * 100.0}%  transperancy";
        }
    }

    public class _5_DynamicDecorator
    {
        public static void Drive()
        {
            var square = new Sqaure(1.23f);
            Console.WriteLine(square.AsString());

            var redSqaure = new ColoredShape(square, "red");
            Console.WriteLine(redSqaure.AsString());

            var transperancyRedSqaure = new TransparentShape(redSqaure, 0.45f);
            Console.WriteLine(transperancyRedSqaure.AsString());
        }

    }
}
