using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.Builder
{


    internal class Builder_1s
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

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }

            public string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                sb.AppendLine($"{i} <{Name}>");
                if (!string.IsNullOrEmpty(Text))
                {
                    sb.Append(new string(' ', indentSize * (indent + 1)));
                    sb.AppendLine(Text);
                }
                foreach (var item in Element)
                {
                    sb.Append(item.ToStringImpl(indent + 1));
                }
                sb.AppendLine($"{i} </{Name}>");
                return sb.ToString();
            }

        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            HtmlElement root = new HtmlElement();

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            public HtmlBuilder AddChild(string childName, string childText)
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
            var hello = "hello";
            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine(hello);
            sb.AppendLine("</p>");
            Console.WriteLine(sb);


            var words = new[] { "hello", "world" };
            sb.Clear();

            sb.Append("<ul>");
            foreach (var item in words)
            {
                sb.AppendFormat("<li>{0}</li>", item);

            }
            sb.Append("</ul>");
            Console.WriteLine(sb);

            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello").AddChild("li", "world");    //  Fluent Builder
            Console.WriteLine(
                builder.ToString());


        }
    }
}
