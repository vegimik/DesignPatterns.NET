using System;

namespace DesignPatterns.NET._1._General
{
    public class Delegate
    {
        public delegate void MyDelegateFunc(string name);
        public static void MyFunc(string name)
        {
            Console.WriteLine(name);
        }

        public static void Drive()
        {
            MyDelegateFunc delegateFunc = MyFunc;
            delegateFunc("Sample");
        }
    }
}
