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
    public interface ILikedProductManager
    {
        IResult GetFavoriteProducts(int UserId);
        IResult AddFavorite(LikedProduct Entity);
    }
}
