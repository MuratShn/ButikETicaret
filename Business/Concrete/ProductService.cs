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
    public class ProductService : IProductManager
    {
        private readonly IProductDal _productManager;
        public ProductService(IProductDal productManager)
        {
            _productManager = productManager;
        }
        public IResult Add(Product Entity)
        {
            var validationResults = ValidationTool<ProductValidator,Product>.Validate(Entity); //validasyon
            if (!validationResults.success)return validationResults;

            var rules = BusinessRules.Rules(UrunEkleme11DenOnceOlmalı(),Test()); //businnes işlemleri
            //if (!rules.success) return rules;

            _productManager.Add(Entity);

            //_productManager.Add(Entity);
            return new SuccessResult("Başarıyla Eklendi");
        }
        public IDataResult<int> LastProduct()
        {
            var result = _productManager.GetAll().OrderByDescending(x => x.Id).First(); //userId ile duzenlenicek
            return new SuccessDataResult<int>(result.Id);
        }

        public IResult Delete(Product Entity)
        {
            _productManager.Delete(Entity);
            return new SuccessResult("Başarıyla Silindi");
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productManager.GetAll());
        }

        public IDataResult<Product> GetById(int id)
        {
            var result = _productManager.Get(x => x.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Product>(null,"Böyle bir ürün bulunmamakta");
            }
            return new SuccessDataResult<Product>(result);
        }

        public IDataResult<List<ProductDetailDto>> GetAllProductDetail()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productManager.GetProductsDetail());
        }

        public IDataResult<ProductDetailDto> GetByIdProductDetail(int id,string color)
        {
            return new SuccessDataResult<ProductDetailDto>(_productManager.GetByProductDetailById(id,color));
        }

        public IDataResult<NonFeatureProductByIdDto> GetByIdNonFeaturesProductDetail(int id)
        {
            return new SuccessDataResult<NonFeatureProductByIdDto>(_productManager.NonFeatureProductDetailById(id));
        }
        
        public IDataResult<List<MyProductDetailDto>> MyProductDetails(int id)
        {
            return new SuccessDataResult<List<MyProductDetailDto>>(_productManager.GetMyProducts(id));
        }




        private IResult UrunEkleme11DenOnceOlmalı()
        {
            if (DateTime.Now.Hour > 11)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Saat 11den sonra ürün eklenemez");
        }
        private IResult Test()
        {
            return new ErrorResult("Test olarak hazırlandı hata");
        }

      
    }
}
