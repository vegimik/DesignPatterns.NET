using System;
using System.Collections.Generic;
using System.Text;
using static DesignPatterns.NET._24._Visitor._3_ClassicVisitor;

//  Double Dispatcher
namespace DesignPatterns.NET._24._Visitor
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;
    internal class _3_ClassicVisitor
    {
        public interface IExpressionVisitor
        {
            void Visit(DoubleExpression doubleExpression);
            void Visit(AdditionExpression additionExpression);
        }
        public abstract class Expression
        {
            public abstract void Accept(IExpressionVisitor expressionVisitor);
        }

        public class DoubleExpression : Expression
        {
            public double value;
            public DoubleExpression(double value)
            {
                this.value = value;
            }

            public override void Accept(IExpressionVisitor expressionVisitor)
            {
                expressionVisitor.Visit(this);
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

            public override void Accept(IExpressionVisitor expressionVisitor)
            {
                expressionVisitor.Visit(this);
            }
        }

        public class ExpressionPrinter : IExpressionVisitor
        {
            StringBuilder sb = new StringBuilder();
            public void Visit(DoubleExpression doubleExpression)
            {
                sb.Append(doubleExpression.value);
            }

            public void Visit(AdditionExpression additionExpression)
            {
                sb.Append("(");
                additionExpression.left.Accept(this);
                sb.Append("+");
                additionExpression.right.Accept(this);
                sb.Append(")");
            }

            public override string ToString()
            {
                return sb.ToString();
            }
        }

        public class ExpressionCalculator : IExpressionVisitor
        {
            public double Result;
            public void Visit(DoubleExpression doubleExpression)
            {
                Result = doubleExpression.value;
            }

            public void Visit(AdditionExpression additionExpression)
            {
                additionExpression.left.Accept(this);
                var a = Result;
                additionExpression.right.Accept(this);
                var b = Result;
                Result = a + b;
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

            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Console.WriteLine(sb);

            var calc = new ExpressionCalculator();
            calc.Visit(e);
            Console.WriteLine($"{ep} = {calc}");
        }

    }
}
