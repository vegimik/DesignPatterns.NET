using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._15._Interpretrs
{
    internal class InterpreterCodingExercise_Method3
    {
        public static Dictionary<char, int> Variables = new Dictionary<char, int>();
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
                Addition, Substraction
            }
            public Type? MyType;
            public IElement Left, Right;
            public bool isCalculatable = true;
            public int Value
            {
                get
                {
                    if (!isCalculatable)
                    {
                        return 0;
                    }
                    else
                        switch (MyType)
                        {
                            case Type.Addition:
                                return Left.Value + Right.Value;
                            case Type.Substraction:
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
                Integer, Plus, Minus, Variable
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

                switch (char.IsLetter(input[i]) ? 'c' : input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case 'c':
                        var sbs = new StringBuilder(input[i].ToString());
                        for (int j = i + 1; j < input.Length; j++)
                        {
                            if (char.IsLetter(input[j]))
                            {
                                sbs.Append(input[j]); ++i;
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Variable, sbs.ToString()));
                                break;
                            }
                        }
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
                                break;
                            }
                        }
                        result.Add(new Token(Token.Type.Integer, sb.ToString()));
                        break;

                }

            }
            return result;
        }


        public static IElement Parse(IReadOnlyList<Token> tokens)
        {
            //Reverse
            //1'.   a+b+c
            //  1.1'. LEFT:     a+b
            //      1.1.1'. LEFT:       a
            //      1.1.2'. Operator:   +
            //      1.1.3'. RIGHT:      b
            //  1.2'. Operator: Substracion
            //  1.3'. RIGHT:    c

            var result = new BinaryOperation();
            bool haveLHS = false;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (!result.isCalculatable)
                    break;
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
                        if (result.MyType != null)
                        {
                            var element = result;
                            result = new BinaryOperation();
                            result.Left = element;
                        }
                        result.MyType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        if (result.MyType != null)
                        {
                            var element = result;
                            result = new BinaryOperation();
                            result.Left = element;
                        }
                        result.MyType = BinaryOperation.Type.Substraction;
                        break;
                    case Token.Type.Variable:
                        var sb = new StringBuilder();
                        for (int j = 0; j < token.Text.Length; j++)
                        {
                            if (Variables.ContainsKey(token.Text[j]))
                            {
                                sb.Append(Variables[token.Text[j]]);
                            }
                            else
                            {
                                result.isCalculatable = false;
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;

        }


        public static void Drive()
        {
            //Calculate("1+2+3")  should return 6

            string input = "1+2+3";

            var tokens = Lex(input);
            Console.WriteLine(String.Join("\n", tokens));


            var parsed = Parse(tokens);
            Console.WriteLine($"{input} = {parsed.Value}");

        }

    }
}
