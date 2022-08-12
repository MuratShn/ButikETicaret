using Business.Abstract;
using Entities.ViewModel_s;
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
    public class ImagesController : ControllerBase
    {
        private readonly IProductImageManager _productImageManager;
        public ImagesController(IProductImageManager productImageManager)
        {
            _productImageManager = productImageManager;
        }

        [HttpPost("addProductImages"), DisableRequestSizeLimit]
        [Authorize(Roles = "SalesPerson")]
        public IActionResult ProductImagesAdd([FromForm]ProductImageVM productImageVM)
        {
            var result = _productImageManager.Add(productImageVM);
            return Ok(result);
        }

    }
}
