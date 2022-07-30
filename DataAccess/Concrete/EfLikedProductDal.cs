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
    public class EfLikedProductDal : EfEntityRepositoryBase<LikedProduct, Context>, ILikedProductDal
    {
        public List<ProductDetailDto> GetFavoriteProducts(int userId)
        {
            using (Context context = new())
            {
                var features = context.ProductFeatures.ToList();
                var favorites = context.LikedProducts.Where(x => x.UserId == userId).Select(x => x.Id).ToList();

                var result = from p in context.Products.Where(x=> favorites.Contains(x.Id))
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
                    var colors = new List<string>();

                    foreach (var item in fet)
                    {
                        if (item.ProductId == res[i].Id)
                        {
                            f2.Add(item);

                            if (!colors.Contains(item.Color))
                            {
                                colors.Add(item.Color);
                            }
                        }
                    }
                    res[i].Colors = colors;
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
    }
}
