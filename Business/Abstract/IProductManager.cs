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
    public interface IProductManager
    {
        IResult Add(Product Entity);
        IResult Delete(Product Entity);
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
        IDataResult<int> LastProduct();

        IDataResult<List<ProductDetailDto>> GetAllProductDetail();
        IDataResult<ProductDetailDto> GetByIdProductDetail(int id, string color);
        IDataResult<NonFeatureProductByIdDto> GetByIdNonFeaturesProductDetail(int id);
        IDataResult<List<MyProductDetailDto>> MyProductDetails(int id);
        IDataResult<List<ProductDetailDto>> getNewProducts(int count);
    }
}
