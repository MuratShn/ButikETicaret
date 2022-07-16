using Business.Abstract;
using Core.Utilities.Results;
using Entities.ViewModel_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductImageService : IProductImageManager
    {
        public IResult Add(List<ProductImageVM> productImages)
        {
            throw new NotImplementedException();
        }
    }
}
