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
        public List<ProductDetailDto> GetProductsDetail()
        {

            using (Context context = new Context())
            {
                var result = from p in context.Products
                             join fea in context.ProductFeatures on p.Id equals fea.ProductId
                             join cat in context.Categories on p.CategoryId equals cat.Id
                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 CategoryName = cat.Name,
                                 Mold = p.Mold,
                                 Material = p.Material,
                                 Gender = p.Gender,
                                 Status = p.Status,
                                 Price = p.Price,
                                 Size = fea.Size,
                                 Color = fea.Color,
                                 Stock = fea.Stock,
                             };
                return result.ToList();
            }
        }

        public NonFeatureProductByIdDto NonFeatureProductDetailById(int id)
        {
            using (Context context = new ())
            {
                var result = from p in context.Products where(p.Id == id)
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
    }
}
