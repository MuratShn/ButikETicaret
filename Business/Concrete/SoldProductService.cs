using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SoldProductService : ISoldProductManager
    {
        private readonly ISoldProductDal _soldProductDal;

        public SoldProductService(ISoldProductDal soldProductDal)
        {
            _soldProductDal = soldProductDal;
        }

        public IResult Add(List<BasketDto> entity,int orderId)
        {
            foreach (var item in entity)
            {
                _soldProductDal.Add(new Entities.Concrete.SoldProduct()
                {
                    OrderId = orderId,
                    NumberOfProduct = item.Quantitiy,
                    ProductFeatureId = item.Feature.Id
                });
            }
            return new SuccessResult();
        }
    }
}
