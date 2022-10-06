using Autofac;
using System;
using static DesignPatterns.NET._6._Adapter.AdapterCodingExercise;

namespace DesignPatterns.NET._6._Adapter
{
    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }
    public class AdapterCodingExercise
    {


        public class Square
        {
            public int Side;
            public int WidthHeightDistance { get; set; }

            public Square()
            {

            }

            public Square(int side, int widthHeightDistance)
            {
                Side = side;
                WidthHeightDistance = widthHeightDistance;
            }
        }

        public interface IRectangle
        {
            int Width { get; }
            int Height { get; }
        }


        public class SquareToRectangleAdapter : IRectangle
        {
            public Square Square { get; set; }
            public SquareToRectangleAdapter(Square square)
            {
                Square = square ?? throw new ArgumentNullException(paramName: nameof(square));
            }

            public int Width => (int)(Square.WidthHeightDistance + Math.Sqrt(Square.WidthHeightDistance + 4 * Square.Side)) / 2;

            public int Height => (int)Math.Abs((Square.WidthHeightDistance - Math.Sqrt(Square.WidthHeightDistance + 4 * Square.Side)) / 2);


            // todo

            public void Calculate()
            {
                Square.Side = ExtensionMethods.Area(this);
            }
        }

        public static void Drive()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<SquareToRectangleAdapter>().As<IRectangle>();
            cb.RegisterType<Square>();

            cb.RegisterAdapter<Square, SquareToRectangleAdapter>((x) => new SquareToRectangleAdapter(x));

            using (var c = cb.Build())
            {
                var square = c.Resolve<Square>();
                square.Side = 12;
                square.WidthHeightDistance = 1;
                var sa = c.Resolve<SquareToRectangleAdapter>();
                sa.Square = square;
                Console.WriteLine(sa.Square.Side);

            }

        }
    }
}
