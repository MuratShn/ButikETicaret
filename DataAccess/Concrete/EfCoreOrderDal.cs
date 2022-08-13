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
    public class EfCoreOrderDal : EfEntityRepositoryBase<Order, Context>, IOrderDal
    {
        public List<MyOrderDto> GetMyOrders(int userId)
        {
            using (Context context = new())
            {
                var orders = context.Orders.Where(x => x.UserId == userId).ToList();
                var result2 = new List<List<OrderDetailDto>>();

                foreach (var item in orders)
                {
                    var result = from sold in context.SoldProducts.Where(x => x.OrderId == item.Id)
                                 join fea in context.ProductFeatures on sold.ProductFeatureId equals fea.Id
                                 join pro in context.Products on fea.ProductId equals pro.Id

                                 select new OrderDetailDto
                                 {
                                     OrderId = item.Id,
                                     ProductId = pro.Id,
                                     Color = fea.Color,
                                     Size = fea.Size,
                                     Price = pro.Price,
                                     ProductName = pro.ProductName,
                                     Quantitiy = sold.NumberOfProduct,
                                 };
                    result2.Add(result.ToList());
                }

                foreach (var item in result2)
                {
                    foreach (var item2 in item)
                    {
                        var path = context.ProductImages.Where(x => x.Color == item2.Color && x.ProductId == item2.ProductId).Select(x => x.ProductPath).FirstOrDefault();

                        byte[] bytes = File.ReadAllBytes(path);
                        string image = Convert.ToBase64String(bytes);
                        item2.Image = image;
                    }
                }

                var sonuc = new List<MyOrderDto>();

                for (int i = 0; i < orders.Count(); i++)
                {
                    MyOrderDto myorderdto = new MyOrderDto()
                    {
                        Id = orders[i].Id,
                        OrderDate = orders[i].OrderDate,
                        OrderPrice = orders[i].Fee,
                        OrderDetail = result2[i]
                    };
                    sonuc.Add(myorderdto);
                }
                
                return sonuc;
            }
        }


        private List<OrderDetailDto> test(string pName, string color, string size, int qt, int price)
        {
            return new();
        }
    }
}
