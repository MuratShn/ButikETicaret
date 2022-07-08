using Business.Abstract;
using Entities.Concrete;
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
    public class ProductFeatureController : ControllerBase
    {
        private readonly IProductFeatureManager _productFeature;
        public ProductFeatureController(IProductFeatureManager productFeature)
        {
            _productFeature = productFeature;
        }
        
        [HttpPost("addFeatures")]
        public IActionResult Add(List<ProductFeature> Entities)
        {
            var result = _productFeature.AddFeatures(Entities);
            return Ok(result);
        }

        [HttpGet("getProductFeature")]
        public IActionResult GetAll()
        {
            var result = _productFeature.GetAll();
            return Ok(result);
        }

        [HttpGet("getProductFeatureById")]
        public IActionResult GeyById(int id)
        {
            var result = _productFeature.GetByProductId(id);
            return Ok(result);
        }


    }
}
