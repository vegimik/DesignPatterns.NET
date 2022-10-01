using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET.SOLID_Principles
{
    //Single Responsibility Principle
    internal class _1_S
    {
        public class Journal
        {
            private readonly List<string> Entries = new List<string>();
            private static int count { get; set; } = 0;

            public int AddEntry(string text)
            {
                Entries.Add($"{count}: {text}");
                return count;//memento
            }

            public void RemoveEntry(int index)
            {
                Entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return String.Join(Environment.NewLine, Entries);
            }
        }

        public class Persistence
        {
            public void saveToFile(Journal j, string fileName, bool _overwrite = false)
            {
                if (_overwrite || File.Exists(fileName))
                {
                    File.WriteAllText(fileName, j.ToString());
                }
            }
        }
        static void Drive()
        {
            var j = new Journal();
            j.AddEntry("Sample 1");
            j.AddEntry("Sample 2");

            var p = new Persistence();
            p.saveToFile(j, "sample/file/name", true);

        }
    }
}
