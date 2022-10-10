using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._9._Decorator
{
    public class _7_StaticDecorator
    {


        public abstract class Shape
        {
            public abstract string AsString();

            public static explicit operator Shape(Type v)
            {
                throw new NotImplementedException();
            }
        }


        public abstract class ShapeDecorator : Shape
        {
            protected internal readonly List<Shape> types = new();
            protected internal readonly Shape shape;

            public ShapeDecorator(Shape type)
            {
                this.shape = shape;
                if (type is ShapeDecorator sd)
                {
                    types.AddRange(sd.types);
                }
            }

            public override string AsString()
            {
                throw new NotImplementedException();
            }
        }

        public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
        {
            protected readonly TCyclePolicy policy = new();

            protected ShapeDecorator(Shape type) : base(type)
            {
                if (policy.TypeAdditionAllowedType((Shape)typeof(TSelf), types))
                {
                    types.Add((Shape)typeof(TSelf));
                }

            }

        }


        public class Circle : Shape
        {
            private float radius;
            public float Radius
            {
                get => radius;
                set => this.radius = value;
            }

            public Circle() : this(0f)
            {

            }
            public Circle(float radius)
            {
                this.radius = radius;
            }

            public void Resize(float factor)
            {
                this.radius *= factor;
            }

            public override string AsString()
            {
                return $"A Circle with radius {radius}";
            }
        }

        public class Sqaure : Shape
        {
            private float side;
            public Sqaure() : this(0.0f)
            {

            }
            public Sqaure(float side)
            {
                this.side = side;
            }

            public void Resize(float factor)
            {
                this.side *= factor;
            }

            public override string AsString()
            {
                return $"A Sqaure with side {side}";
            }
        }

        public class ShapeDecoratorWithPolicy<T> :
            ShapeDecorator<T, ThrowOnCyclePolicy>
        {
            public ShapeDecoratorWithPolicy(Shape type) : base(type)
            {

            }

        }

        public class ColoredShape : ShapeDecorator<ColoredShape, CycleAllowedPolicy>
        //ShapeDecoratorWithPolicy<ColoredShape>
        {

            private string color;

            public ColoredShape(Shape type, string color) : base(type)
            {
                type = type ?? throw new ArgumentNullException(paramName: nameof(type));
                color = color ?? throw new ArgumentNullException(paramName: nameof(color));
            }

            public string AsString()
            {
                var sb = new StringBuilder($"{shape.AsString()}");
                if (policy.ApplicationAllowed(shape, types))
                {
                    sb.Append($" has the color {color}");
                }
                return sb.ToString();
            }
        }



        public class TransparentShape : Shape
        {
            private Shape shape;
            private float transperancy;

            public TransparentShape(Shape shape, float transperancy)
            {
                this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
                this.transperancy = transperancy;
            }

            public override string AsString()
            {
                return $"{shape.AsString()} has {transperancy * 100.0}%  transperancy";
            }
        }



        public abstract class ShapeDecoratorCyclePolicy
        {
            public abstract bool TypeAdditionAllowedType(Shape type, IList<Shape> alltypes);
            public abstract bool ApplicationAllowed(Shape type, IList<Shape> alltypes);


        }

        public class CycleAllowedPolicy : ShapeDecoratorCyclePolicy
        {
            public override bool ApplicationAllowed(Shape type, IList<Shape> alltypes)
            {
                return true;
            }

            public override bool TypeAdditionAllowedType(Shape type, IList<Shape> alltypes)
            {
                return true;
            }
        }

        public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
        {
            private bool handler(Shape type, IList<Shape> alltypes)
            {
                if (alltypes.Contains(type))
                {
                    throw new InvalidOperationException($"Cycle detected! Type is already a {type}");
                }
                return true;
            }
            public override bool ApplicationAllowed(Shape type, IList<Shape> alltypes)
            {
                return handler(type, alltypes);
            }

            public override bool TypeAdditionAllowedType(Shape type, IList<Shape> alltypes)
            {
                return handler(type, alltypes);
            }
        }

        public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
        {
            public override bool ApplicationAllowed(Shape type, IList<Shape> alltypes)
            {

                return true;
            }

            public override bool TypeAdditionAllowedType(Shape type, IList<Shape> alltypes)
            {

                return !alltypes.Contains(type);
            }
        }

        //public class ColoredShape<T> : T// there is a problem called CRTP
        //  CRPT => The curiously recurring template pattern

        public class ColoredShape<T> : Shape
            where T : Shape, new()
        {
            private string color;

            private T shape = new T();

            public ColoredShape() : this("black")
            {

            }

            public ColoredShape(string color)
            {
                this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
            }

            public override string AsString()
            {
                return $"{shape.AsString()} has the color {color}";
            }
        }

        public class TransparentShape<T> : Shape
            where T : Shape, new()
        {
            private float transperancy;
            private T shape = new T();

            public TransparentShape() : this(0f)
            {

            }

            public TransparentShape(float transperancy)
            {
                this.transperancy = transperancy;
            }

            public override string AsString()
            {
                return $"{shape.AsString()} has {transperancy * 100.0}%  transperancy";
            }
        }

        public static void Drive()
        {

            var redSqaure = new ColoredShape<Sqaure>("red");

            Console.WriteLine(redSqaure.AsString());

            var circle = new TransparentShape<ColoredShape<Circle>>(0.4f);

            Console.WriteLine(circle.AsString());

        }
    }


}
