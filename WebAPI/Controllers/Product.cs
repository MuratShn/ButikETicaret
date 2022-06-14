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
    public class Product : ControllerBase
    {
        private readonly IProductManager _productManager;
        public Product(IProductManager productManager)
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
    }
}
