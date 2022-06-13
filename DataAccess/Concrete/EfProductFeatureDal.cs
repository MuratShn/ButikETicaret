using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfProductFeatureDal : EfEntityRepositoryBase<ProductFeature, Context>, IProductFeatureDal
    {
    }
}
