using Business.Abstract;
using Business.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
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
        private readonly IProductDal _productManager;
        
        public ProductFeatureService(IProductFeatureDal productFeatureManager,IProductDal productManager)
        {
            _productFeatureManager = productFeatureManager;
            _productManager = productManager;
        }

        public IResult Add(ProductFeature Entity)
        {
            _productFeatureManager.Add(Entity);
            return new SuccessResult("Ekleme Başarılı");
        }
        public IResult AddFeatures(List<ProductFeature> Entities)
        {
            var stock = 0;
            int productId = 0;

            if (Entities.Count <= 0)
                return new ErrorResult("Varyant eklemelisiniz");

            foreach (var item in Entities)
            {
                var validationResults = ValidationTool<ProductFeatureValidator, ProductFeature>.Validate(item); //validasyon

                if (!validationResults.success)
                    return validationResults;


                productId = item.ProductId;
                stock += item.Stock;
            }
            var rules = BusinessRules.Rules(checkProduct(productId));

            int featureStock = _productFeatureManager.GetAll(x => x.ProductId == productId).Select(x => x.Stock).Sum();
            int productStock = _productManager.Get(x => x.Id == productId).Stok;



            if (!rules.success)
            {
                return rules;
            }

            if (productStock >= featureStock + stock)
            {
                foreach (var item in Entities)
                {
                    _productFeatureManager.Add(item);
                }
                return new SuccessResult("Ekleme Başarılı");
            }
            else
            {
                    return new ErrorResult($"Stoklarda uyuşmazlık oldu eklemek isteğiniz Stok: {featureStock} Zaten Eklenmiş Olan Stok: {stock}  Ürünün Stoku: {productStock}");
            }


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

        public IResult ReduceStock(List<BasketDto> Entities)
        {
            foreach (var item in Entities)
            {
                var dbEntity = _productFeatureManager.Get(x => x.Id == item.Feature.Id);
                
                if (dbEntity.Stock > item.Quantitiy)
                { 
                    dbEntity.Stock = dbEntity.Stock - item.Quantitiy;
                    _productFeatureManager.Update(dbEntity);
                }
                else
                    throw new Exception("Olmaz Böyle şey");

            }
            return new SuccessResult("Stock azaltma başarılı");
        }

        private IResult checkProduct(int id) //hangi ürüne özellik eklenecekse o ürünü varmı yokmu bakıyoz
        {
            if(_productManager.GetAll().Any(x => x.Id == id))
            {
                return new SuccessResult();
            }
            return new ErrorResult("Ürün Bulunmamaktadır");

        }
    }
}
