using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.NET._4._Prototype.PrototypeCodingExercise.Program;

namespace DesignPatterns.NET._4._Prototype
{
    public static class DeepCopyExtension
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> self) where T : new()
        {
            return self.DeepCopy();
        }
        public static T DeepCopy<T>(this T line) where T : Line, new()
        {
            return ((IDeepCopyable<T>)line).DeepCopy();
        }
    }
    internal class PrototypeCodingExercise
    {

        public interface IDeepCopyable<T> where T : new()
        {
            void CopyTo(T target);

            T DeepCopy()
            {
                T t = new T();
                CopyTo(t);
                return t;
            }
        }


        public class Program
        {
            public interface ICloneable<T> where T : class
            {
                T DeepCopy();
            }
            public class Point : ICloneable<Point>
            {
                public int X, Y;

                public Point()
                {

                }
                public Point(int x, int y)
                {
                    X = x;
                    Y = y;
                }

                public Point DeepCopy()
                {
                    return new Point(X, Y);
                }

                public override string ToString()
                {
                    return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
                }
            }

            public class Line : ICloneable<Line>
            {
                public Point Start, End;

                public Line()
                {
                }

                public Line(Point start, Point end)
                {
                    Start = start;
                    End = end;
                }

                public Line DeepCopy()
                {
                    return new Line(Start.DeepCopy(), End.DeepCopy());
                }

                public override string ToString()
                {
                    return $"Point {nameof(Start)}: {Start}, Point {nameof(End)}: {End}";
                }
            }

            static void Drive()
            {
                var line = new Line(new Point(1, 2), new Point(3, 4));
                Console.WriteLine(line.ToString());

                var copyLine = line.DeepCopy();
                //copyLine.End = new Point(5, 6);
                copyLine.End.Y = 6;
                Console.WriteLine(copyLine.ToString());



            }
        }
    }

}