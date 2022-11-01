using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.NET._24._Visitor.VisitorCodingExercise;

namespace DesignPatterns.NET._24._Visitor
{
    internal class VisitorCodingExercise
    {

        public abstract class ExpressionVisitor
        {
            // todo
            public abstract void Visit(Value value);

            public abstract void Visit(AdditionExpression ae);

            public abstract void Visit(MultiplicationExpression me);
        }

        public abstract class Expression
        {
            public abstract void Accept(ExpressionVisitor ev);
        }

        public class Value : Expression
        {
            public readonly int TheValue;

            public Value(int value)
            {
                TheValue = value;
            }

            public override void Accept(ExpressionVisitor ev)
            {
                ev.Visit(this);
            }

            // todo
        }

        public class AdditionExpression : Expression
        {
            public readonly Expression LHS, RHS;

            public AdditionExpression(Expression lhs, Expression rhs)
            {
                LHS = lhs;
                RHS = rhs;
            }

            public override void Accept(ExpressionVisitor ev)
            {
                ev.Visit(this);
            }

            // todo
        }

        public class MultiplicationExpression : Expression
        {
            public readonly Expression LHS, RHS;

            public MultiplicationExpression(Expression lhs, Expression rhs)
            {
                LHS = lhs;
                RHS = rhs;
            }

            public override void Accept(ExpressionVisitor ev)
            {
                ev.Visit(this);
            }

            // todo
        }

        public class ExpressionPrinter : ExpressionVisitor
        {
            StringBuilder sb = new StringBuilder();
            public override void Visit(Value value)
            {
                sb.Append(value.TheValue.ToString().Trim());
            }

            public override void Visit(AdditionExpression ae)
            {
                sb.Append("(");
                ae.LHS.Accept(this);
                sb.Append("+");
                ae.RHS.Accept(this);
                sb.Append(")");
            }

            public override void Visit(MultiplicationExpression me)
            {
                //sb.Append("+");
                me.LHS.Accept(this);
                sb.Append("*");
                me.RHS.Accept(this);
            }

            public override string ToString()
            {
                return sb.ToString();
            }
        }

        public static void Drive()
        {

            var simple = new AdditionExpression(new Value(2), new Value(3));
            var ep = new ExpressionPrinter();
            ep.Visit(simple);
            Console.WriteLine($"{ep.ToString()} = (2 + 3)");
            //Assert.That(ep.ToString(), Is.EqualTo("(2+3)"));
        }

    }

    [TestFixture]
    public class VisitorCodingExercise_TestSuits
    {
        [Test]
        public void SimpleAddition()
        {
            var simple = new AdditionExpression(new Value(2), new Value(3));
            var ep = new ExpressionPrinter();
            ep.Visit(simple);
            Assert.That(ep.ToString(), Is.EqualTo("(2+3)"));
        }

        [Test]
        public void ProductOfAdditionAndValue()
        {
            var expr = new MultiplicationExpression(
              new AdditionExpression(new Value(2), new Value(3)),
              new Value(4)
              );
            var ep = new ExpressionPrinter();
            ep.Visit(expr);
            Assert.That(ep.ToString(), Is.EqualTo("(2+3)*4"));
        }
    }

}
