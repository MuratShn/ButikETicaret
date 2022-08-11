using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductFeatureManager
    {
        IResult Add(ProductFeature Entity);
        IDataResult<List<ProductFeature>> GetAll();
        IDataResult<List<ProductFeature>> GetByProductId(int Id);
        IResult AddFeatures(List<ProductFeature> Entities);
        IResult ReduceStock(List<BasketDto> Entities);

    }
}
