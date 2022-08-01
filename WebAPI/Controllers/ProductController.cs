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

        [HttpGet("getAllProductDetail")]
        public IActionResult GetAllProductDetail()
        {
            var result = _productManager.GetAllProductDetail();
            return Ok(result);
        }

        [HttpPost("productAdd")]
        public IActionResult Add(Product product)
        {
            var result = _productManager.Add(product);
            return Ok(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _productManager.GetById(id);
            return Ok(result);
        }
        
        [HttpGet("getLastProduct")]
        public IActionResult LastProduct()
        {
            var result = _productManager.LastProduct();
            return Ok(result);
        }
        
        [HttpGet("getMyProduct")]
        [Authorize(Roles = "Customer")]
        public IActionResult MyProductDetail(int id)
        {
            var result = _productManager.MyProductDetails(id);
            return Ok(result);
        }
        
    
        [HttpGet("getByIdProductDetail")]
        public IActionResult GetByIdProductDetail(int id,string color)
        {
            var result = _productManager.GetByIdProductDetail(id,color);
            return Ok(result);
        }

        [HttpGet("getNewProducts")]
        public IActionResult NewProducts(int Count)
        {
            var result = _productManager.getNewProducts(Count);
            return Ok(result);
        }

    }
}
