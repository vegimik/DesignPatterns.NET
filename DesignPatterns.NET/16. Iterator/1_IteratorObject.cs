using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._16._Iterator
{
    internal class _1_IteratorObject
    {
        public class Node<T>
        {
            public T Value;
            public Node<T> Left, Right;
            public Node<T> parent;
            public Node(T value)
            {
                Value = value;
            }
            public Node(T value, Node<T> left, Node<T> right) : this(value)
            {
                Value = value;
                Left = left;
                Right = right;
                left.parent = right.parent = this;
            }
        }

        public class InOrderIterator<T>
        {
            private readonly Node<T> root;
            public Node<T> Current;
            private bool yieldedStart;

            public InOrderIterator(Node<T> current)
            {
                this.root = current;
                Current = current;
                while (Current.Left != null)
                    Current = Current.Left;
            }

            public bool MoveNext()
            {
                if (!yieldedStart)
                {
                    yieldedStart = true;
                    return true;
                }

                if (Current.Right != null)
                {
                    Current = Current.Right;
                    while (Current.Left != null)
                        Current = Current.Left;

                    return true;

                }
                else
                {
                    var parent = Current.parent;
                    while (parent != null && Current == parent.parent)
                    {
                        Current = parent;
                        parent = parent.parent;
                    }
                    Current = parent;
                    return Current != null;
                }

            }

            public void Reset()
            {

            }
        }



        public static void Drive()
        {
            ///*    1
            ///    / \
            ///   2   3
            ///
            /// in-order: 213
            /// 

            var root = new Node<int>(1,
                new Node<int>(2), new Node<int>(3));
            var it = new InOrderIterator<int>(root);
            while (it.MoveNext())
            {
                Console.WriteLine(it.Current.Value);
                Console.WriteLine(",");
            }

        
        }
    }
}
