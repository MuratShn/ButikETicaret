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
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager _addressManager;

        public AddressController(IAddressManager addressManager)
        {
            _addressManager = addressManager;
        }
        [HttpPost("addAddress")] 
        public IActionResult AddressAdd(Address address)
        {
            var userId = Convert.ToInt32(User.Identities.First().Name);
            var result = _addressManager.AddAddress(userId, address);
            return Ok(result);
        }
        [HttpDelete("deleteAddress")]
        public IActionResult AddressDelete(int AddressId)
        {
            var userId = Convert.ToInt32(User.Identities.First().Name);
            var result = _addressManager.deleteAddress(AddressId,userId);
            return Ok(result);
        }
        [HttpGet("getAllAddress")]
        public IActionResult GetAllAddress() //user address
        {
            var userId = Convert.ToInt32(User.Identities.First().Name);
            var result = _addressManager.getAllAddres(userId);
            return Ok(result);
        }
    }
}
