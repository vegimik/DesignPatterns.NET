using Autofac;
using System;
    
namespace DesignPatterns.NET._7._Bridge
{
    internal class _1_Bridge
    {

        public interface IRenderer
        {
            void RenderCircle(float radius);
        }

        public class VectorRenderer : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.WriteLine($"Drawing a circle of radius {radius}");
            }
        }

        public class RasterRender : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.WriteLine($"Drawing pixels for circle with radius {radius}");
            }
        }

        public abstract class Shape
        {
            protected IRenderer renderer;
            public Shape(IRenderer renderer)
            {
                this.renderer = renderer;
            }

            public abstract void Draw();
            public abstract void Resize(float factor);
        }

        public class Circle : Shape
        {
            private float radius;
            public Circle(IRenderer renderer, float radius) : base(renderer)
            {
                this.radius = radius;
            }
            public override void Draw()
            {
                renderer.RenderCircle(radius);
            }

            public override void Resize(float factor)
            {
                radius *= factor;
            }
        }



        static void Main(string[] args)
        {

            //IRenderer renderer = new RasterRender();
            IRenderer renderer = new VectorRenderer();
            var circle = new Circle(renderer, 5);
            circle.Draw();
            circle.Resize(3);
            circle.Draw();

            var cb = new ContainerBuilder();
            //cb.RegisterType<RasterRender>().As<IRenderer>();
            cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
            cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));

            using (var c = cb.Build())
            {
                var crc = c.Resolve<Circle>(
                    new PositionalParameter(0, 5.0f));
                crc.Draw();
            }

        }

    }
}
