using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//You are asked to implement the Builder design pattern for rendering simple chunks of code.

//Sample use of the builder you are asked to create:

//var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
//Console.WriteLine(cb);
//The expected output of the above code is:

//public class Person
//{
//    public string Name;
//    public int Age;
//}
//Please observe the same placement of curly braces and use two-space indentation.

namespace DesignPatterns.NET.Builder
{
    internal class BuilderCodingExercise
    {


        public class HtmlElement
        {
            public string Name, Text;
            public List<HtmlElement> Element { get; set; } = new List<HtmlElement>();

            private const int indentSize = 2;

            public HtmlElement()
            {

            }
            public HtmlElement(string name, string text)
            {
                Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
                Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }

            public string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);


                sb.Append($"{i}public {Text ?? "class"} {Name}");
                if (string.IsNullOrEmpty(Text))
                {
                    sb.AppendLine("").AppendLine("{");
                }
                else
                    sb.AppendLine(";");


                foreach (var item in Element)
                {
                    sb.Append(item.ToStringImpl(indent + 1));
                }

                if (string.IsNullOrEmpty(Text))
                    sb.AppendLine("}");

                return sb.ToString();
            }

        }

        public class CodeBuilder
        {
            private readonly string rootName;
            HtmlElement root = new HtmlElement();

            public CodeBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            public CodeBuilder AddField(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Element.Add(e);
                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }


            public void Clear()
            {
                root = new HtmlElement
                {
                    Name = rootName
                };
            }

        }



        public static void Drive()
        {
            var builder = new CodeBuilder("Person");
            builder.AddField("Name", "string").AddField("Age", "int");    //  Fluent Builder
            Console.WriteLine(
                builder.ToString());


        }
    }
}
