using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._16._Iterator
{
    internal class _2_IteratorMethod
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


        public class BinearyTree<T> 
        {
            private Node<T> root;
            public BinearyTree(Node<T> root)
            {
                this.root = root;
            }

            public IEnumerable<Node<T>> InOrder
            {
                get
                {
                    IEnumerable<Node<T>> Traverse(Node<T> current)
                    {
                        if (current.Left != null)
                        {
                            foreach (var left in Traverse(current.Left))
                            {
                                yield return left;
                            }

                        }
                        yield return current;

                        if (current.Right != null)
                        {
                            foreach (var right in Traverse(current.Right))
                            {
                                yield return right;
                            }

                        }
                    }

                    foreach (var node in Traverse(root))
                    {
                        yield return node;
                    }
                }
            }

            InOrderIterator<T> GetEnumertor()
            {
                return new InOrderIterator<T>(root);
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

            var tree = new BinearyTree<int>(root);

            Console.WriteLine(String.Join(", ", tree.InOrder.Select(x => x.Value)));
        }

    }
}
