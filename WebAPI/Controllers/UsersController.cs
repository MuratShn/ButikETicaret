using Business.Abstract;
using Entities.ViewModel_s;
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
    public class UsersController : ControllerBase
    {
        private readonly IIdentityManager _identityManager;

        public UsersController(IIdentityManager identityManager)
        {
            _identityManager = identityManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(CreateUserVM user)
        {
            var result = await _identityManager.Add(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async  Task<IActionResult> Login(UserLoginVM user)
        {
            var result = await _identityManager.Login(user);
            return Ok(result);
        }
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(string role)
        {
            var result = await _identityManager.AddRole(role);
            return Ok(result);
        }
        [HttpGet("getUserProfile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.Identities.First().Name;
            var result = await _identityManager.GetUserProfile(userId);
            return Ok(result);
        }
    }
}
