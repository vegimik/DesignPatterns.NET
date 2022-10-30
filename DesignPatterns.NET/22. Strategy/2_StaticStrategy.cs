using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.NET._22._Strategy
{
    internal class _2_StaticStrategy
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

        public class TextProcessor<LS> where LS : IListStrategy, new()
        {
            public StringBuilder sb = new StringBuilder();
            public IListStrategy listStrategy = new LS();

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

            public static implicit operator TextProcessor<LS>(TextProcessor<HtmlListStrategy> v)
            {
                return new TextProcessor<HtmlListStrategy>();
            }

            public static implicit operator TextProcessor<LS>(TextProcessor<MarkdownListStartegy> tp)
            {
                return new TextProcessor<MarkdownListStartegy>();
            }
        }

        public static void Drive()
        {
            var tp = new TextProcessor<MarkdownListStartegy>();
            tp.AppenList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(tp);


            tp = new TextProcessor<HtmlListStrategy>();
            tp.AppenList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(tp);
        }
    }
}
