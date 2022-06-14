using Business.Abstract;
using Business.FluentValidation;
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
            if (!rules.success) return rules;


            _productManager.Add(Entity);
            return new SuccessResult("Başarıyla Eklendi");
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
            return new SuccessDataResult<Product>(_productManager.Get(x => x.Id == id));
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
