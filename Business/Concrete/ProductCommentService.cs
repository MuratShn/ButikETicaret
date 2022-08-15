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
    public class ProductCommentService : IProductCommentManager
    {
        private readonly IProductCommentDal _productCommentDal;

        public ProductCommentService(IProductCommentDal productCommentDal)
        {
            _productCommentDal = productCommentDal;
        }

        public IResult Add(ProductComment Entity)
        {
            var comment = _productCommentDal.Get(x => x.UserId == Entity.UserId && x.ProductId == Entity.ProductId && x.Color == Entity.Color);
            if (comment == null)
            {
                Entity.Date = DateTime.Now;

                _productCommentDal.Add(Entity);
                return new SuccessResult("Yorumunuz Eklenmiştir");
            }
            else
            {
                return new ErrorResult("Bu Ürünü Daha Önce Yorum Yaptınız");
            }

        }

        public IResult GetMyComment(int userId)
        {
            var result = _productCommentDal.GetMyComments(userId);
            return new SuccessDataResult<List<MyCommentDto>>(result);
        }

        public IResult GetProductComment(int productId)
        {
            var result = _productCommentDal.GetProductComments(productId);
            return new SuccessDataResult<List<ProductCommentDto>>(result);
        }
    }
}
