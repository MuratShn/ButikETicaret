using DataAccess.Concrete;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context();
            var result = context.Products;
            Console.WriteLine("Hello World!");
        }
    }
}
