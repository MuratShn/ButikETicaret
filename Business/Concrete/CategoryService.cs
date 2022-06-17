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
    public class CategoryService : ICategoryManager
    {
        private readonly ICategoryDal _categoryService;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryService = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryService.GetAll());
        }
    }
}
