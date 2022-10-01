using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Factories
{
    public class FactoryMethod
    {
        static void Drive()
        {
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);


        }

    }


    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public class Point
    {
        private double x, y;

        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }
        public static Point NewPolarPoint(double x, double y)
        {
            return new Point(x * Math.Cos(y), x * Math.Sin(y));
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
