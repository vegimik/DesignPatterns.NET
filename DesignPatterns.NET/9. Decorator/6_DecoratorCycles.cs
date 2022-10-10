using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._9._Decorator
{
    internal class _6_DecoratorCycles
    {

        public interface IShape
        {
            string AsString();
        }

        public interface IType : IShape
        {
        }

        public abstract class ShapeDecorator : IShape
        {
            protected internal readonly List<IType> types = new();
            protected internal readonly IType type;

            public ShapeDecorator(IType type)
            {
                this.type = type;
                if (type is ShapeDecorator sd)
                {
                    types.AddRange(sd.types);
                }
            }

            public string AsString()
            {
                throw new NotImplementedException();
            }
        }

        public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
        {
            protected readonly TCyclePolicy policy = new();

            protected ShapeDecorator(IType type) : base(type)
            {
                if (policy.TypeAdditionAllowedType((IType)typeof(TSelf), types))
                {
                    types.Add((IType)typeof(TSelf));
                }

            }

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

        public class ShapeDecoratorWithPolicy<T> :
            ShapeDecorator<T, ThrowOnCyclePolicy>
        {
            public ShapeDecoratorWithPolicy(IType type) : base(type)
            {

            }

        }

        public class ColoredShape : ShapeDecorator<ColoredShape, CycleAllowedPolicy>
        //ShapeDecoratorWithPolicy<ColoredShape>
        {

            private string color;

            public ColoredShape(IType type, string color) : base(type)
            {
                type = type ?? throw new ArgumentNullException(paramName: nameof(type));
                color = color ?? throw new ArgumentNullException(paramName: nameof(color));
            }

            public string AsString()
            {
                var sb = new StringBuilder($"{type.AsString()}");
                if (policy.ApplicationAllowed(type, types))
                {
                    sb.Append($" has the color {color}");
                }
                return sb.ToString();
            }
        }


        public class TransparentShape : IShape
        {
            private IShape shape;
            private float transperancy;

            public TransparentShape(IShape shape, float transperancy)
            {
                this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
                this.transperancy = transperancy;
            }

            public string AsString()
            {
                return $"{shape.AsString()} has {transperancy * 100.0}%  transperancy";
            }
        }



        public abstract class ShapeDecoratorCyclePolicy
        {
            public abstract bool TypeAdditionAllowedType(IType type, IList<IType> alltypes);
            public abstract bool ApplicationAllowed(IType type, IList<IType> alltypes);


        }

        public class CycleAllowedPolicy : ShapeDecoratorCyclePolicy
        {
            public override bool ApplicationAllowed(IType type, IList<IType> alltypes)
            {
                return true;
            }

            public override bool TypeAdditionAllowedType(IType type, IList<IType> alltypes)
            {
                return true;
            }
        }

        public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
        {
            private bool handler(IType type, IList<IType> alltypes)
            {
                if (alltypes.Contains(type))
                {
                    throw new InvalidOperationException($"Cycle detected! Type is already a {type}");
                }
                return true;
            }
            public override bool ApplicationAllowed(IType type, IList<IType> alltypes)
            {
                return handler(type, alltypes);
            }

            public override bool TypeAdditionAllowedType(IType type, IList<IType> alltypes)
            {
                return handler(type, alltypes);
            }
        }

        public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
        {
            public override bool ApplicationAllowed(IType type, IList<IType> alltypes)
            {

                return true;
            }

            public override bool TypeAdditionAllowedType(IType type, IList<IType> alltypes)
            {

                return !alltypes.Contains(type);
            }
        }

        public static void Drive()
        {

            var circle = new Circle(2);

            var colored1 = new ColoredShape((IType)circle, "red");

            var colored2 = new ColoredShape((IType)circle, "blue");


        }
    }


}
