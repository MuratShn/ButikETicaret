﻿using Core.Utilities.Results;
using DataAccess.Concrete;
using Entities.Concrete;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var product = new Product
            //{
            //    Material = "TestSil",
            //    CategoryId = 1,
            //    Gender = true,
            //    Mold = "TestSil",
            //    Price = 10,
            //    Status = true,
            //    ProductName = "TestSil",
            //    Stok = 10
            //};
            //EfProductDal ef = new EfProductDal();

            //ef.Add(product);

            //var s =buss(Test1(6), Test2(2));
            //Console.WriteLine(s);
            
            Console.ReadKey();
        }
        public static bool buss(params bool[] results)
        {
            foreach (var item in results)
            {
                if (!item)
                {
                    return item;
                }
            }
            return true;
        }
        public static bool Test1(int id)
        {
            if (id > 5) return true;
            return false;
        }
        public static bool Test2(int id)
        {
            if (id > 3) return true;
            return false;
        }
    }
}
