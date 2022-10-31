using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._24._Visitor
{
    internal class _1_IntrusiveVisitor
    {

        public abstract class Expression
        {
            public abstract void Print(StringBuilder sb);
        }

        public class DoubleExpression : Expression
        {
            private double value;
            public DoubleExpression(double value)
            {
                this.value = value;
            }

            public override void Print(StringBuilder sb)
            {
                sb.Append(value);
            }
        }

        public class AdditionExpression : Expression
        {
            Expression left, right;
            public AdditionExpression(Expression left, Expression right)
            {
                this.left = left ?? throw new ArgumentNullException(nameof(left));
                this.right = right ?? throw new ArgumentNullException(nameof(right));
            }

            public override void Print(StringBuilder sb)
            {
                sb.Append("(");
                left.Print(sb);
                sb.Append("+");
                right.Print(sb);
                sb.Append(")");
            }
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
            e.Print(sb);
            Console.WriteLine(sb);

        }
    }
}
