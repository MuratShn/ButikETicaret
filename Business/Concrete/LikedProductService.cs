using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LikedProductService : ILikedProductManager
    {
        private readonly ILikedProductDal _likedProductDal;

        public LikedProductService(ILikedProductDal likedProductDal)
        {
            _likedProductDal = likedProductDal;
        }

        public IResult AddFavorite(LikedProduct Entity)
        {
            var favorite = _likedProductDal.GetAll(x => x.UserId == Entity.UserId & x.ProductId == Entity.ProductId);
            
            if (favorite.Count == 0)
            {
                _likedProductDal.Add(Entity);
                return new SuccessResult("Favorilere Eklendi");
            }
            else
            {
                return new ErrorResult("Ürün zaten favorilerde");
            }

        }

        public IResult GetFavoriteProducts(int UserId)
        {
            var result = _likedProductDal.GetFavoriteProducts(UserId);
            return new SuccessDataResult<List<ProductDetailDto>>(result);
        }

        public IResult RemoveFavorite(int ProductId, int UserId)
        {
            var result = _likedProductDal.Get(x => x.ProductId == ProductId && x.UserId == UserId);
            if (result is null)
            {
                return new ErrorResult("Hata");
            }
            _likedProductDal.Delete(result);
            return new SuccessResult("Favorilerden kaldırıldı");
        }
    }
}
