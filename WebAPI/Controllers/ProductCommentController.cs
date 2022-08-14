using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCommentController : ControllerBase
    {
        private readonly IProductCommentManager _productComment;

        public ProductCommentController(IProductCommentManager productComment)
        {
            _productComment = productComment;
        }

        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add(ProductComment Entity)
        {
            Entity.UserId = Convert.ToInt32(User.Identities.First().Name);

            var result = _productComment.Add(Entity);
            return Ok(result);
        }

        [HttpGet("getMyComments")]
        [Authorize]
        public IActionResult GetMyComments()
        {
            var userId = Convert.ToInt32(User.Identities.First().Name);
            var result = _productComment.GetMyComment(userId);
            return Ok(result);
        }
    }
}
