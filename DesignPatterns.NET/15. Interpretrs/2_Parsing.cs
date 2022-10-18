using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._15._Interpretrs
{
    internal class _2_Parsing
    {
        public interface IElement
        {
            int Value { get; }
        }

        public class Integer : IElement
        {
            public int Value { get; }
            public Integer(int value)
            {
                Value = value;
            }
        }

        public class BinaryOperation : IElement
        {
            public enum Type
            {
                Addition, Subsctraction
            }
            public Type MyType;
            public IElement Left, Right;
            public int Value
            {
                get
                {
                    switch (MyType)
                    {
                        case Type.Addition:
                            return Left.Value + Right.Value;
                        case Type.Subsctraction:
                            return Left.Value - Right.Value;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

        }

        public class Token
        {
            public enum Type
            {
                Integer, Plus, Minus, Lparen, Rparen
            }

            public Type MyType;
            public string Text;

            public Token(Type myType, string text)
            {
                MyType = myType;
                Text = text;
            }

            public override string ToString()
            {
                return $"{nameof(MyType)}: {MyType}, ${nameof(Text)}: {Text}";
            }
        }


        public static List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(Token.Type.Lparen, "("));
                        break;
                    case ')':
                        result.Add(new Token(Token.Type.Rparen, ")"));
                        break;
                    default:
                        var sb = new StringBuilder(input[i].ToString());
                        for (int j = i + 1; j < input.Length; j++)
                        {
                            if (char.IsDigit(input[j]))
                            {
                                sb.Append(input[j]); ++i;
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Integer, sb.ToString()));
                                break;
                            }
                        }
                        break;

                }

            }
            return result;
        }


        public static IElement Parse(IReadOnlyList<Token> tokens)
        {
            //Reverse
            //1'.(13+4)-(12+1)
            //  1.1'. LEFT:     (13+4)
            //      1.1.1'. LEFT:       13
            //      1.1.2'. Operator:   +
            //      1.1.3'. RIGHT:      4
            //  1.2'. Operator: Substracion
            //  1.3'. RIGHT:    (12+1)
            //      1.3.1'. LEFT:       12
            //      1.3.2'. Operator:   +
            //      1.3.3'. RIGHT:      1

            var result = new BinaryOperation();
            bool haveLHS = false;
            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                switch (token.MyType)
                {
                    case Token.Type.Integer:
                        var integer = new Integer(int.Parse(token.Text));
                        if (!haveLHS)
                        {
                            result.Left = integer;
                            haveLHS = true;
                        }
                        else
                        {
                            result.Right = integer;
                        }
                        break;
                    case Token.Type.Plus:
                        //result = new BinaryOperation
                        //{
                        //    Left = result,
                        //    MyType = BinaryOperation.Type.Addition
                        //};
                        result.MyType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.MyType = BinaryOperation.Type.Subsctraction;
                        break;
                    case Token.Type.Lparen:
                        var j = i;
                        for (; j < tokens.Count; j++)
                        {
                            if (tokens[j].MyType == Token.Type.Rparen)
                            {
                                break;
                            }
                        }
                        var subExpression = tokens.Skip(i + 1).Take(j - i).ToList();
                        var element = Parse(subExpression);
                        if (!haveLHS)
                        {
                            result.Left = element;
                            haveLHS = true;
                        }
                        else
                        {
                            result.Right = element;
                        }
                        i = j;
                        break;
                    default:
                        break;
                }
            }
            return result;

        }


        public static void Drive()
        {
            string input = "(13+4)-(12+1)";

            var tokens = Lex(input);
            Console.WriteLine(String.Join("\n", tokens));


            var parsed = Parse(tokens);
            Console.WriteLine($"{input} = {parsed.Value}");

        }
    }
}
