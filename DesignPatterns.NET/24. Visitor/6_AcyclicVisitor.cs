﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._24._Visitor
{
    internal class _6_AcyclicVisitor
    {

        public interface IVisitor<TVisitable>
        {
            void Visit(TVisitable obj);
        }

        public interface IVisitor { } // marker interface

        public abstract class Expression
        {
            public virtual void Accept(IVisitor visitor)
            {
                if (visitor is IVisitor<Expression> typed)
                    typed.Visit(this);
            }
        }

        public class DoubleExpression : Expression
        {
            public double Value;

            public DoubleExpression(double value)
            {
                Value = value;
            }

            public override void Accept(IVisitor visitor)
            {
                if (visitor is IVisitor<DoubleExpression> typed)
                    typed.Visit(this);
            }
        }

        public class AdditionExpression : Expression
        {
            public Expression Left;
            public Expression Right;

            public AdditionExpression(Expression left, Expression right)
            {
                Left = left;
                Right = right;
            }

            public override void Accept(IVisitor visitor)
            {
                if (visitor is IVisitor<AdditionExpression> typed)
                    typed.Visit(this);
            }
        }

        public class ExpressionPrinter : IVisitor,
          IVisitor<Expression>,
          IVisitor<DoubleExpression>,
          IVisitor<AdditionExpression>
        {
            StringBuilder sb = new StringBuilder();

            public void Visit(DoubleExpression de)
            {
                sb.Append(de.Value);
            }

            public void Visit(AdditionExpression ae)
            {
                sb.Append("(");
                ae.Left.Accept(this);
                sb.Append("+");
                ae.Right.Accept(this);
                sb.Append(")");
            }

            public void Visit(Expression obj)
            {
                // default handler?
            }

            public override string ToString() => sb.ToString();
        }

        public static void Drive()
        {
            var e = new AdditionExpression(
              left: new DoubleExpression(1),
              right: new AdditionExpression(
                left: new DoubleExpression(2),
                right: new DoubleExpression(3)));
            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Console.WriteLine(ep.ToString());
        }
    }
}
