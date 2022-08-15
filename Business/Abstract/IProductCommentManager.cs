using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductCommentManager
    {
        IResult Add(ProductComment Entity);
        IResult GetMyComment(int UserId);
        IResult GetProductComment(int productId);
    }
}
