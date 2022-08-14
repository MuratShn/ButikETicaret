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
    public class ProductCommentService : IProductCommentManager
    {
        private readonly IProductCommentDal _productCommentDal;

        public ProductCommentService(IProductCommentDal productCommentDal)
        {
            _productCommentDal = productCommentDal;
        }

        public IResult Add(ProductComment Entity)
        {
            var comment = _productCommentDal.Get(x => x.UserId == Entity.UserId && x.ProductId == x.ProductId);
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
            var result = _productCommentDal.GetAll(x => x.UserId == userId);

            throw new NotImplementedException();
        }

        public IResult GetProductComment()
        {
            throw new NotImplementedException();
        }
    }
}
