using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.NET._24._Visitor._2_ReflectiveVisitor;

namespace DesignPatterns.NET._24._Visitor
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;
    internal class _2_ReflectiveVisitor
    {

        public abstract class Expression
        {
        }

        public class DoubleExpression : Expression
        {
            public double value;
            public DoubleExpression(double value)
            {
                this.value = value;
            }

        }

        public class AdditionExpression : Expression
        {
            public Expression left, right;
            public AdditionExpression(Expression left, Expression right)
            {
                this.left = left ?? throw new ArgumentNullException(nameof(left));
                this.right = right ?? throw new ArgumentNullException(nameof(right));
            }

            //public override void Print(StringBuilder sb)
            //{
            //    sb.Append("(");
            //    left.Print(sb);
            //    sb.Append("+");
            //    right.Print(sb);
            //    sb.Append(")");
            //}
        }

        public static class ExpressionPrinter
        {
            private static DictType actions = new DictType
            {
                [typeof(DoubleExpression)] = (e, sb) =>
                {
                    var de = (DoubleExpression)e;
                    sb.Append(de.value);
                },
                [typeof(AdditionExpression)] = (e, sb) =>
                {
                    var ae = (AdditionExpression)e;
                    sb.Append("(");
                    Print(ae.left, sb);
                    sb.Append("+");
                    Print(ae.left, sb);
                    sb.Append(")");
                }
            };
            public static void Print(Expression e, StringBuilder sb)
            {
                actions[e.GetType()](e, sb);
            }

            //public static void Print(Expression e, StringBuilder sb)
            //{
            //    if (e is DoubleExpression de)
            //    {
            //        sb.Append(de.value);
            //    }
            //    else if (e is AdditionExpression ae)
            //    {
            //        sb.Append("(");
            //        Print(ae.left, sb);
            //        sb.Append("+");
            //        Print(ae.left, sb);
            //        sb.Append(")");
            //    }
            //}
        }

        public static void Drive()
        {
            var e = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)
                    )
                );
            var sb = new StringBuilder();
            //e.Print(sb);
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb);

        }
    }
}
