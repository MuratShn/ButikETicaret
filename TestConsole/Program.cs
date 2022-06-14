using DataAccess.Concrete;
using Entities.Concrete;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = new Product
            {
                Material = "TestSil",
                CategoryId = 1,
                Gender = true,
                Mold = "TestSil",
                Price = 10,
                Status = true,
                ProductName = "TestSil",
                Stok = 10
            };
            EfProductDal ef = new EfProductDal();

            ef.Add(product);
        }
    }
}
