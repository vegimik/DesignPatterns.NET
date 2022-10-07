using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.NET._8._Composite
{
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {
        public IEnumerator<IValueContainer> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }

    }

    public static class CompositeExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }
    public class CompositeCodingExercise
    {
        public static void Drive()
        {
            var singleValue = new SingleValue();
            singleValue.Value = 1;

            var manyVlaues = new ManyValues();
            manyVlaues.AddRange(new List<int> { 1, 2, 3 });

            var result = manyVlaues.Sum();
            Console.WriteLine(result);
            result += singleValue.Sum();
            Console.WriteLine(result);


        }
    }
}
