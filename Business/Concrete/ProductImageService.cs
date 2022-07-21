using Business.Abstract;
using Business.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.ViewModel_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductImageService : IProductImageManager
    {
        private readonly IProductImageDal _productImageManager;
        private readonly IConfiguration _configuration;
        public ProductImageService(IProductImageDal productImageManager, IConfiguration configuration)
        {
            _productImageManager = productImageManager;
            _configuration = configuration;
        }
        public IResult Add(ProductImageVM productImages)
        {
            var validationResults = ValidationTool<ProductImageValidator, ProductImageVM>.Validate(productImages); //validasyon

            if (!validationResults.success) return validationResults;

            var rules = BusinessRules.Rules(CheckImage(productImages), CheckSizeFile(productImages)); //businnes işlemleri
            if (!rules.success)
                return rules;

            var code = _configuration.GetSection("StoredFilesPathProduct").Value;

            foreach (var item in productImages.Image)
            {
                var guid = Guid.NewGuid();
                var ımagePath = guid.ToString() + "+" + item.FileName;
                var path = Path.Combine(code, ımagePath);

                var entity = new ProductImage { Color = productImages.Color, ProductId = productImages.ProductId, ProductPath = path };

                _productImageManager.Add(entity);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    item.CopyTo(stream);
                }

            }



            
            return new SuccessResult("Ürün Başarıyla Eklendi");
        }

        public IResult GetByProductIdOneImage(int productId)
        {
            var result = _productImageManager.Get(x => x.ProductId == productId);
            return new SuccessDataResult<ProductImage>(result);
        }

        private IResult CheckImage(ProductImageVM productImages)
        {
            if (productImages.Image == null)
            {
                return new ErrorResult("En az bir tane seçim yapmak zorundasınız");
            }
            return new SuccessResult();
        }
        private IResult CheckSizeFile(ProductImageVM productImages)
        {
            foreach (var item in productImages.Image)
            {
                if (item.Length > 1292555 || item.Length < 2)
                {
                    return new ErrorResult("Resim boyutu hatası");
                }
            }
            return new SuccessResult();
        }
    }
}
