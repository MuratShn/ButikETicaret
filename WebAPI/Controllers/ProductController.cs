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
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var s = _productManager.GetAll();
            var deleteVariable = s.data.Find(x => x.ProductName == "TestSil");


            //var y = _productManager.Delete(deleteVariable);

            return Ok();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _productManager.GetAll();
            return Ok(result);
        }
        [HttpGet("getAllDetail")]
        public IActionResult GetAllDetail()
        {
            var result = _productManager.GetAllProductDetailDto();
            return Ok(result);
        }
        [HttpGet("getByIdDetail")]
        public IActionResult GetByIdDetail()
        {
            var result = _productManager.GetAllProductDetailDto();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            var result = _productManager.Add(product);
            return Ok(result);
        }
    }
}
