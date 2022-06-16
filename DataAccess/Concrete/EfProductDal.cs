using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfProductDal : EfEntityRepositoryBase<Product, Context>, IProductDal
    {
        public ProductDetailDto GetByProductDetailById(int id)
        {

            using (Context context = new())
            {
                var features = from p in context.Products
                               where (p.Id == id)
                               join fea in context.ProductFeatures on p.Id equals fea.ProductId
                               select new ProductFeature
                               {
                                   Id = fea.Id,
                                   Color = fea.Color,
                                   ProductId = p.Id,
                                   Size = fea.Size,
                                   Stock = fea.Stock
                               };

                var result = from p in context.Products
                             where (p.Id == id)
                             join cat in context.Categories on p.CategoryId equals cat.Id
                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 Features = features.ToList(),
                                 Gender = p.Gender,
                                 CategoryName = cat.Name,
                                 Material = p.Material,
                                 Mold = p.Mold,
                                 Price = p.Price,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Stok = p.Stok
                             };

                return result.First();
            }
        }

        public List<ProductDetailDto> GetProductsDetail()
        {
            using (Context context = new())
            {
                var features = context.ProductFeatures.ToList();

                var result = from p in context.Products
                             join cat in context.Categories on p.CategoryId equals cat.Id
                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 Gender = p.Gender,
                                 CategoryName = cat.Name,
                                 Material = p.Material,
                                 Mold = p.Mold,
                                 Price = p.Price,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Stok = p.Stok
                             };
                var res = result.ToList();
                var fet = features.ToList();

                for (int i = 0; i < res.Count(); i++)
                {
                    var f2 = new List<ProductFeature>();

                    foreach (var item in fet)
                    {
                        if (item.ProductId == res[i].Id)
                        {
                            f2.Add(item);
                        }
                    }
                    res[i].Features = f2;
                }

                return res;
            }
        }

        public NonFeatureProductByIdDto NonFeatureProductDetailById(int id)
        {
            using (Context context = new())
            {
                var result = from p in context.Products
                             where (p.Id == id)
                             join cat in context.Categories on p.CategoryId equals cat.Id
                             select new NonFeatureProductByIdDto
                             {
                                 Id = p.Id,
                                 CategoryName = cat.Name,
                                 Gender = p.Gender,
                                 Material = p.Material,
                                 Mold = p.Mold,
                                 Price = p.Price,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Stok = p.Stok
                             };
                return result.First();
            }
        }


        //public List<ProductDetailDtos> GetProductsDetail()
        //{

        //    using (Context context = new Context())
        //    {
        //        var result = from p in context.Products
        //                     join fea in context.ProductFeatures on p.Id equals fea.ProductId
        //                     join cat in context.Categories on p.CategoryId equals cat.Id
        //                     select new ProductDetailDtos
        //                     {
        //                         Id = p.Id,
        //                         ProductName = p.ProductName,
        //                         CategoryName = cat.Name,
        //                         Mold = p.Mold,
        //                         Material = p.Material,
        //                         Gender = p.Gender,
        //                         Status = p.Status,
        //                         Price = p.Price,
        //                         Size = fea.Size,
        //                         Color = fea.Color,
        //                         Stock = fea.Stock,
        //                     };
        //        return result.ToList();
        //    }
        //}

    }
}
