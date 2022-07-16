using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
            var code = _configuration.GetSection("StoredFilesPathProduct").Value;
            var guid = Guid.NewGuid();
            var ımagePath = guid.ToString() + "+" + productImages.Image[0].FileName;
            var path = Path.Combine(code, ımagePath);
            
            using (var stream = new FileStream(path,FileMode.Create))
            {
                productImages.Image[0].CopyTo(stream);
            }

            //var rules = BusinessRules.Rules(CheckImage(productImages)); //businnes işlemleri
            return null;
        }

        public IResult TesT()
        {
            return new SuccessResult();
        }

        private IResult CheckImage(ProductImageVM productImages)
        {
            if (productImages.Image.All(x => x.Length < 1))
            {
                return new ErrorResult("En az bir tane seçim yapmak zorundasınız");
            }
            return new SuccessResult();
        }
    }
}
