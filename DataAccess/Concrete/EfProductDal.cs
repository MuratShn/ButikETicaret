using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfProductDal : EfEntityRepositoryBase<Product, Context>, IProductDal
    {

        public List<MyProductDetailDto> GetMyProducts(int id)
        {
            //Refactoring gelicek birden fazla ürün dönücek
            //Refactroing geldi ürünler userıdye göre geliyo temsili şekil altta verildi
            #region RESULT ÖRNEĞİ
            /*
            Birden Fazla Donucek

            {categoryName: "Tişört"
            colors: (3) ['Kırmızı', 'Mavi', 'Yeşil'] //liste 
            gender: "1"
            id: 1
            material: "Pamuk"
            mold: "Oversize"
            price: 120
            productName: "Kısa Kollu Pamuk Tişört"
            sizes: (3) ['Smal', 'Medium', 'Large'] //liste 
            status: true
            stok: 20}
            .....
             
             */
            #endregion


            using (Context context1 = new())
            {
                var products = from p in context1.Products
                               where (p.UserId == id)
                               join c in context1.Categories on p.CategoryId equals c.Id
                               select new MyProductDetailDto()
                               {
                                   Id = p.Id,
                                   Gender = p.Gender,
                                   CategoryName = c.Name,
                                   Material = p.Material,
                                   Mold = p.Mold,
                                   Price = p.Price,
                                   ProductName = p.ProductName,
                                   Status = p.Status,
                                   Stok = p.Stok
                               };
                var features = from p in context1.Products
                               where (p.UserId == id)
                               join fea in context1.ProductFeatures on p.Id equals fea.ProductId
                               select new ProductFeature
                               {
                                   Id = fea.Id,
                                   Color = fea.Color,
                                   ProductId = p.Id,
                                   Size = fea.Size,
                                   Stock = fea.Stock
                               };

                var pro = products.ToList();
                var feat = features.ToList();


                foreach (var item in pro)
                {
                    List<string> color = new List<string>();
                    List<string> size = new List<string>();

                    foreach (var fea in feat)
                    {
                        if (item.Id == fea.ProductId)
                        {
                            if (!(size.Contains(fea.Size.ToString())))
                            {
                                size.Add(fea.Size.ToString());
                                Console.WriteLine(fea.Color.ToString());

                            }
                            if (!(color.Contains(fea.Color.ToString())))
                            {
                                color.Add(fea.Color.ToString());
                                Console.WriteLine(fea.Size.ToString());

                            }
                        }
                    }

                    item.Sizes = size;
                    item.Colors = color;
                }

                return pro;
            }

        }

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
                var res = result.ToList();

                var images = context.ProductImages.ToList();

                if (images.Count > 0)
                {
                    foreach (var item in res)
                    {
                        var imagesList = new List<ImageDto>();
                        foreach (var image in images)
                        {
                            if (image.ProductId == item.Id)
                            {
                                var path = image.ProductPath;
                                byte[] bytes = File.ReadAllBytes(path);
                                string image2 = Convert.ToBase64String(bytes);
                                var dto = new ImageDto() { Color = image.Color, Image = image2 };
                                imagesList.Add(dto);
                            }
                        }
                        item.Image = imagesList;
                    }
                }


                return res.First();
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

                var images = context.ProductImages.ToList();

                if (images.Count > 0)
                {
                    foreach (var item in res)
                    {
                        var imagesList = new List<ImageDto>();
                        foreach (var image in images)
                        {
                            if (image.ProductId == item.Id)
                            {
                                var path = image.ProductPath;
                                byte[] bytes = File.ReadAllBytes(path);
                                string image2 = Convert.ToBase64String(bytes);
                                var dto = new ImageDto() { Color = image.Color, Image = image2 };
                                imagesList.Add(dto);
                            }
                        }
                        item.Image = imagesList;
                    }
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

    }
}
