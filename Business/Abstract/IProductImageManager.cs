using Core.Utilities.Results;
using Entities.ViewModel_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductImageManager
    {
        IResult Add(List<ProductImageVM> productImages);
    }
}
