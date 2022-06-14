using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductFeatureService : IProductFeatureManager
    {
        private readonly IProductFeatureDal _productFeatureManager;
        
        public ProductFeatureService(IProductFeatureDal productFeatureManager)
        {
            _productFeatureManager = productFeatureManager;
        }

        public IResult Add(ProductFeature Entity)
        {
            _productFeatureManager.Add(Entity);
            return new SuccessResult("Ekleme Başarılı");
        }

        public IDataResult<List<ProductFeature>> GetAll()
        {
            return new SuccessDataResult<List<ProductFeature>>(_productFeatureManager.GetAll());
        }

        public IDataResult<ProductFeature> GetById(int Id)
        {
            return new SuccessDataResult<ProductFeature>(_productFeatureManager.Get(x => x.Id == Id));
        }

        public IDataResult<List<ProductFeature>> GetByProductId(int Id)
        {
            var result = _productFeatureManager.GetAll(x => x.ProductId == Id);
            return new SuccessDataResult<List<ProductFeature>>(result);
        }
    }
}
