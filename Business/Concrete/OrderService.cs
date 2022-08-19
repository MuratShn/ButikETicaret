using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Concrete
{
    public class OrderService : IOrderManager
    {
        private readonly IOrderDal _orderDal;
        private readonly ISoldProductManager _soldProductManager;
        private readonly IProductFeatureManager _productFeatureManager;

        public OrderService(IOrderDal orderDal, ISoldProductManager soldProductManager, IProductFeatureManager productFeatureManager)
        {
            _orderDal = orderDal;
            _soldProductManager = soldProductManager;
            _productFeatureManager = productFeatureManager;
        }

        public IResult Add(OderDto entity)
        {
            using(TransactionScope scope = new())
            {
                try
                {
                    entity.OrderDate = DateTime.Now;
                    var order = new Entities.Concrete.Order() { AddressId = entity.AddressId, Fee = entity.Fee, OrderDate = entity.OrderDate, UserId = entity.UserId };

                    var result1 = _productFeatureManager.ReduceStock(entity.SoldProducts); //stokları azalttık

                    if (result1.success)
                        _orderDal.Add(order);
                    else
                        throw new Exception("Olmaz Böyle şey OrderDa Patladık");

                    var result2 = _soldProductManager.Add(entity.SoldProducts, order.Id);

                    if (!result2.success)
                    {
                        throw new Exception("Hata Oluştu");
                    }

                    return new SuccessResult("Sipariş Başarıyla Alınmıştır");
                }
                catch (Exception)
                {
                    return new ErrorResult("Hata Meydana Geldi");
                }
            }

        }

        public IResult GetMyOrders(int userId)
        {
            var result = _orderDal.GetMyOrders(userId);
            return new SuccessDataResult<List<MyOrderDto>>(result);
        }
    }
}
