using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._6._Adapter
{
    internal class _2_AdapterCaching
    {

        public class Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(Point)) return false;
                return base.Equals((Point)obj);
            }

            public override string ToString()
            {
                return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
            }
        }

        public class Line
        {
            public Point Start;
            public Point End;

            public Line(Point start, Point end)
            {
                this.Start = start ?? throw new ArgumentNullException(paramName: nameof(start));
                this.End = end ?? throw new ArgumentNullException(paramName: nameof(end));
            }

            public override bool Equals(object obj)
            {
                var other = (Line)obj;
                return Equals(Start, other.Start) && Equals(End, other.End);
            }
        }

        public class VectorObject : Collection<Line>
        {

        }

        public class VectorRectangle : VectorObject
        {
            public VectorRectangle(int x, int y, int width, int height)
            {
                Add(new Line(new Point(x, y), new Point(x + width, y)));
                Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
                Add(new Line(new Point(x, y), new Point(x, y + height)));
                Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
            }
        }

        public class LineToPointAdapter : IEnumerable<Point> //: Collection<Point>
        {
            private static int count = 0;
            static Dictionary<int, List<Point>> cache = new Dictionary<int, List<Point>>();

            public LineToPointAdapter(Line line)
            {
                if (line == null)
                    throw new ArgumentNullException(paramName: nameof(line));

                var hash = line.GetHashCode();
                if (cache.ContainsKey(hash)) return;


                Console.WriteLine($"{++count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");

                var points = new List<Point>();

                int left = Math.Min(line.Start.X, line.End.X);
                int right = Math.Max(line.Start.X, line.End.X);
                int top = Math.Min(line.Start.Y, line.End.Y);
                int bottom = Math.Max(line.Start.Y, line.End.Y);
                int dx = right - left;
                int dy = line.End.Y - line.Start.Y;

                if (dx == 0)
                {
                    for (int y = top; y <= bottom; ++y)
                    {
                        points.Add(new Point(left, y));
                    }
                }
                else if (dy == 0)
                {
                    for (int x = left; x <= right; ++x)
                    {
                        points.Add(new Point(x, top));
                    }
                }
            }

            public IEnumerator<Point> GetEnumerator()
            {
                return cache.Values.SelectMany(x => x).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
    {
      new VectorRectangle(1, 1, 10, 10),
      new VectorRectangle(3, 3, 6, 6)
    };

        // the interface we have
        public static void DrawPoint(Point p)
        {
            Console.Write(".");
        }

        public static void Drive()
        {
            Draw();
            Draw();
        }

        private static void Draw()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                }
            }
        }
    }
}
