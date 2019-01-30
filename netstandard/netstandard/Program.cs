using Lib;
using System;

namespace netstandard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Class1 class1 = new Class1();

            class1.MyProperty = 1;

            var c = class1.Serialize("asfdhgreh");
        }
    }
}
