using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._15._Interpretrs
{
    internal class InterpreterCodingExercise_Method2
    {
        public class ExpressionProcessor
        {
            public Dictionary<char, int> Variables = new Dictionary<char, int>();

            public int Calculate(string expression)
            {
                int result = 0;
                for (int i = 0; i < expression.Length;)
                {
                    var expressionItem = expression[i];
                    if (char.IsDigit(expressionItem))
                    {
                        //  TODO: we have to implement code for calculate algerbric opration
                        var j = i;
                        for (; j < expression.Length; j++)
                        {
                            if (!char.IsDigit(expression[j]))
                            {
                                break;
                            }
                        }
                        var subExpression = expression.Substring(i, j - i);//expression.Skip(i + 1).Take(j - 1).ToString();
                        if (i - 1 > 0 && expression[i - 1] == '-')
                            result -= int.Parse(subExpression);
                        else
                            result += int.Parse(subExpression);
                        i = j;

                    }

                    else if (!(expressionItem == '+' || expressionItem == '-'))
                    {

                        //TODO: we have to check for existence of variables, if exist then calculate
                        //
                        //
                        //otherwise return 0

                        var j = i;
                        for (; j < expression.Length; j++)
                        {
                            if (expression[j] == '+' || expression[j] == '-')
                            {
                                break;
                            }
                        }
                        var subExpression = expression.Substring(i, j - i); //expression.Skip(i + 1).Take(j - 1).ToString();

                        foreach (var item in subExpression)
                        {
                            if (!Variables.ContainsKey(item)) return 0;
                            subExpression = subExpression.Replace($"{item}", Variables[item].ToString());
                        }
                        if (i - 1 > 0 && expression[i - 1] == '-')
                            result -= Calculate(subExpression);
                        else
                            result += Calculate(subExpression);
                        i = j;
                    }
                    else
                    {
                        i++;
                    }


                }
                return result;
            }
        }


        public static void Drive()
        {
            //Calculate("1+2+3")  should return 6
            //Calculate("1+2+xy")  should return 0
            //Calculate("10-2-x")  when x = 3 is in Variables should return 5
            var ep = new ExpressionProcessor();
            ep.Variables.Add('x', 1);
            ep.Variables.Add('y', 3);
            var result = ep.Calculate("1+2-xy");
            Console.WriteLine(result);

        }
    }
}
