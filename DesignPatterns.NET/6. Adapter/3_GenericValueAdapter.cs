﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._6._Adapter
{
    internal class _3_GenericValueAdapter
    {
        public interface IInteger
        {
            int Value { get; }
        }

        public static class Dimensions
        {
            public class Two : IInteger
            {
                public int Value => 2;
            }
            public class Three : IInteger
            {
                public int Value => 3;
            }

        }

        public class Vector<TSelf, T, D>
            where TSelf : Vector<TSelf, T, D>, new()
            where T : new()
            where D : IInteger, new()
        {
            protected T[] data;
            public Vector()
            {
                data = new T[new D().Value];
            }

            public Vector(params T[] values)
            {
                var requiredSize = new D().Value;
                data = new T[requiredSize];

                var providedSize = values.Length;
                for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
                {
                    data[i] = values[i];
                }
            }

            public static TSelf Create(params T[] values)
            {
                var result = new TSelf();
                var requiredSize = new D().Value;
                result.data = new T[requiredSize];

                var providedSize = values.Length;
                for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
                {
                    result.data[i] = values[i];
                }
                return result;
            }

            public T this[int index]
            {
                get => data[index];
                set => data[index] = value;
            }

            public T X
            {
                get => data[0];
                set => data[0] = value;
            }
        }


        public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D>
            where D : IInteger, new()
        {
            public VectorOfInt()
            {

            }

            public VectorOfInt(params int[] values) : base(values)
            {

            }

            public static VectorOfInt<D> operator +
                (VectorOfInt<D> lhs, VectorOfInt<D> rhs)
            {
                var result = new VectorOfInt<D>();
                var dimension = new D().Value;
                for (int i = 0; i < dimension; i++)
                {
                    result[i] = lhs.X + rhs.X;
                }
                return result;
            }
        }

        public class VectorOfFloat<TSelf, D> : Vector<TSelf, float, D>
            where D : IInteger, new()
            where TSelf: Vector<TSelf, float, D>, new()
        {
            public VectorOfFloat()
            {

            }

            public VectorOfFloat(params float[] values) : base(values)
            {

            }

            public static VectorOfFloat<TSelf, D> operator +
                (VectorOfFloat<TSelf, D> lhs, VectorOfFloat<TSelf, D> rhs)
            {
                var result = new VectorOfFloat<TSelf, D>();
                var dimension = new D().Value;
                for (int i = 0; i < dimension; i++)
                {
                    result[i] = lhs.X + rhs.X;
                }
                return result;
            }
        }

        public class Vector2i : VectorOfInt<Dimensions.Two>// Vector<int, Dimensions.Two>
        {
            public Vector2i()
            {

            }

            public Vector2i(params int[] values) : base(values)
            {

            }

        }

        public class Vector3f : VectorOfFloat<Vector3f, Dimensions.Three>
        {
            public Vector3f()
            {

            }

            public Vector3f(params float[] values) : base(values)
            {

            }

            public override string ToString()
            {
                return $"{string.Join(", ", data)}";
            }
        }

        public static void Drive()
        {
            var v = new Vector2i();
            v[0] = 0;

            var vv = new Vector2i();
            vv.X = 0;

            var result = v + vv;


            var u = Vector3f.Create(3.5f, 2.2f, 1);
            VectorOfFloat<Vector3f, Dimensions.Three> u1 = Vector3f.Create(3.5f, 2.2f, 1);
        }
    }
}
