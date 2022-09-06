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

        public static class PointFactory
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

        public class Point
        {
            private double x, y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        static async void Drive()
        {
            var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);


        }
    }

}
