﻿using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductFeatureManager
    {
        IResult Add(ProductFeature Entity);
        IDataResult<List<ProductFeature>> GetAll();
        IDataResult<List<ProductFeature>> GetByProductId(int Id);
    }
}