using Business.Abstract;
using Entities.DTO_s;
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
    [Authorize]
    public class OrderTransactions : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderTransactions(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpPost("Buy")]
        public IActionResult Buy(OderDto order)
        {
            order.UserId = Convert.ToInt32(User.Identities.First().Name);
            var result = _orderManager.Add(order);
            return Ok(result);
        }
    }
}
