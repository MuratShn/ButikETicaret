using Business.Abstract;
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
    public class LikedProductController : ControllerBase
    {
        private readonly ILikedProductManager productLikedService_;
        public LikedProductController(ILikedProductManager productLikedService_)
        {
            this.productLikedService_ = productLikedService_;
        }

        [HttpPost("AddFavorites")]
        public IActionResult AddFavorites()
        {
            return Ok();
        }

    }
}
