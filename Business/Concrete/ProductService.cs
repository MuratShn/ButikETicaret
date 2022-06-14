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
    public class ProductService : IProductManager
    {
        private readonly IProductDal _productManager;
        public ProductService(IProductDal productManager)
        {
            _productManager = productManager;
        }
        public IGeneralResult Add(Product Entity)
        {
            _productManager.Add(Entity);
            return new SuccessResult("Başarıyla Eklendi");
        }

        public IGeneralResult Delete(Product Entity)
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
    }
}
