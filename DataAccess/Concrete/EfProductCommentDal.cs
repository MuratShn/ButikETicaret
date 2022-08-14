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
    public class EfProductCommentDal : EfEntityRepositoryBase<ProductComment, Context>, IProductCommentDal
    {
        public List<MyCommentDto> GetMyComments(int userId)
        {
            using (Context context = new())
            {
                var res = from com in context.ProductComments.Where(x => x.UserId == userId)
                             join p in context.Products on com.ProductId equals p.Id
                             select new MyCommentDto
                             {
                                 Color = com.Color,
                                 Comment = com.Comment,
                                 Date = com.Date,
                                 Id = com.Id,
                                 ProductId = com.ProductId,
                                 ProductName = p.ProductName
                             };

                var result = res.ToList();
                
                foreach (var item in result)
                {
                    var path = context.ProductImages.Where(x => x.ProductId == item.ProductId && x.Color == item.Color).Select(x=>x.ProductPath).FirstOrDefault();
                    if (path != null)
                    {
                        byte[] bytes = File.ReadAllBytes(path);
                        string image = Convert.ToBase64String(bytes);
                        item.Image = image;
                    }
                    else
                    {
                        item.Image = null;
                    }
                }
                return result.ToList();
            }
        }
    }
}
