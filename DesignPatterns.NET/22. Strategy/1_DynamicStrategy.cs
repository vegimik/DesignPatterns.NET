using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.NET._22._Strategy
{
    internal class _1_DynamicStrategy
    {
        public enum OutputFormat
        {
            Markdown,
            Html
        }

        public interface IListStrategy
        {
            void Start(StringBuilder sb);
            void End(StringBuilder sb);
            void AddListItem(StringBuilder sb, string item);
        }

        public class HtmlListStrategy : IListStrategy
        {
            public void AddListItem(StringBuilder sb, string item)
            {
                sb.Append($"    <li>{item}</li>");
            }

            public void End(StringBuilder sb)
            {
                sb.Append("</ul>");
            }

            public void Start(StringBuilder sb)
            {
                sb.Append("<ul>");
            }
        }

        public class MarkdownListStartegy : IListStrategy
        {
            public void AddListItem(StringBuilder sb, string item)
            {
                sb.Append($" * {item}");
            }

            public void End(StringBuilder sb)
            {
            }

            public void Start(StringBuilder sb)
            {
            }
        }

        public class TextProcessor
        {
            public StringBuilder sb = new StringBuilder();
            public IListStrategy listStrategy;

            public void SetOutputFormat(OutputFormat format)
            {
                switch (format)
                {
                    case OutputFormat.Markdown:
                        listStrategy = new MarkdownListStartegy();
                        break;
                    case OutputFormat.Html:
                        listStrategy = new HtmlListStrategy();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(format), format, null);
                }
            }

            public void AppenList(IEnumerable<string> items)
            {
                listStrategy.Start(sb);
                foreach (var item in items)
                    listStrategy.AddListItem(sb, item);
                listStrategy.End(sb);
            }

            public StringBuilder Clear()
            {
                return sb.Clear();
            }
            public override string ToString()
            {
                return sb.ToString();
            }
        }

        public static void Drive()
        {
            var tp = new TextProcessor();
            tp.SetOutputFormat(OutputFormat.Markdown);
            tp.AppenList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(tp);


            tp.Clear();
            tp.SetOutputFormat(OutputFormat.Html);
            tp.AppenList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(tp);
        }
    }
}
