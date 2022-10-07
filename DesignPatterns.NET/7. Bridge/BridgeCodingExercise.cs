using Autofac;
using System;

namespace DesignPatterns.NET._7._Bridge
{
    public class BridgeCodingExercise
    {
        public interface IRenderer
        {
            string WhatToRenderAs { get; }
        }

        public class RasterRenderer : IRenderer
        {
            public string WhatToRenderAs => "Roaster";
        }
        public class VectorRenderer : IRenderer
        {
            public string WhatToRenderAs => "Vector";
        }

        public abstract class Shape
        {
            public IRenderer Renderer { get; private set; }
            public string Name { get; set; }
            public string DrawLike { get; set; }

            public Shape(IRenderer renderer)
            {
                Renderer = renderer;
            }

            public Shape(IRenderer renderer, string name)
            {
                Renderer = renderer;
                Name = name;
            }
            public override string ToString() => $"Drawing {Name} as {DrawLike}";
        }

        public class Triangle : Shape
        {
            public Triangle(IRenderer renderer) : base(renderer)
            {
                Name = "Triangle";
                DrawLike = "pixels";
            }
        }

        public class Square : Shape
        {
            public Square(IRenderer renderer) : base(renderer)
            {
                Name = "Square";
                DrawLike = "lines";
            }
        }

        public class VectorSquare : Square
        {
            public VectorSquare(IRenderer renderer) : base(renderer)
            {
            }

            public override string ToString() => "Drawing {Name} as lines";
        }

        public class RasterSquare : Square
        {
            public RasterSquare(IRenderer renderer) : base(renderer)
            {
            }

            public override string ToString() => "Drawing {Name} as pixels";
        }



        public class VectorTriangle : Triangle
        {
            public VectorTriangle(IRenderer renderer) : base(renderer)
            {
            }

            public override string ToString() => "Drawing {Name} as lines";
        }

        public class RasterTriangle : Square
        {
            public RasterTriangle(IRenderer renderer) : base(renderer)
            {
            }

            public override string ToString() => "Drawing {Name} as pixels";
        }


        static void Main(string[] args)
        {

            var result = new Triangle(new RasterRenderer()).ToString(); // returns "Drawing Triangle as pixels"
            Console.WriteLine(result);


            var cb = new ContainerBuilder();
            cb.RegisterType<RasterRenderer>().As<IRenderer>();
            //cb.RegisterAdapter<IRenderer, Triangle>((ir) => new Triangle(ir));
            cb.Register((c) => new Triangle(c.Resolve<IRenderer>()));

            using (var c = cb.Build())
            {
                var triangle = c.Resolve<Triangle>();
                Console.WriteLine(triangle.ToString());
            }


        }
    }
}
