using System;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Factories
{
    public class Factory
    {

        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }


        public class Point
        {
            private double x, y;

            private Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }

            public static Point Origin => new Point(0, 0);
            public static Point Origin2 = new Point(0, 0); // better

            public static class Factory
            {
                public static Point NewCartesianPoint(double x, double y)
                {
                    return new Point(x, y);
                }
                public static Point NewPolarPoint(double x, double y)
                {
                    return new Point(x * Math.Cos(y), x * Math.Sin(y));
                }
            }
        }

        static async void Drive()
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);

            var origin = Point.Origin2;


        }
    }

}
