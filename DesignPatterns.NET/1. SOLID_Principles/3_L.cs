using System;

namespace DesignPatterns.NET.SOLID_Principles
{
    //Liskov Substitution Principle
    internal class _3_L
    {
        public class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public Rectangle()
            {

            }
            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        public class Square : Rectangle
        {
            /*new*/
            public override int Width
            {
                set
                {
                    base.Width = base.Height = value;
                }
            }
            /*new*/
            public override int Height
            {
                set
                {
                    base.Width = base.Height = value;
                }
            }

        }

        public static int Area(Rectangle r) => r.Width * r.Height;

        static void Drive()
        {
            Rectangle rect = new Rectangle();
            Console.WriteLine($"{rect} has area {Area(rect)}");

            Square sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{rect} has area {Area(rect)}");
        }
    }
}
