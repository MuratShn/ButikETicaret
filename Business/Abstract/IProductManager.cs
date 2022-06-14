using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductManager
    {
        IGeneralResult Add(Product Entity);
        IGeneralResult Delete(Product Entity);
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
    }
}
