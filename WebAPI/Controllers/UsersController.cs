using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Entities.DTO_s;
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
       
        
        [HttpPost("googleLogin")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginVm user)
        {
            var result = await _identityManager.GoogleLogin(user);
            return Ok(result);
        }
        
        [HttpPost("addRole")]
        [Authorize]
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

            if (userId == null) //authorize koymadık onun yerine böyle bi kotrnol sagladık
                return Ok(new ErrorResult("Giriş yapılmamıştır"));
                //return BadRequest("Giriş yapılmamıştır");

            var result = await _identityManager.GetUserProfile(userId);
            return Ok(result);
        }

        [HttpPost("refreshTokenLogin")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] AccessToken refreshToken)
        {
            var result = await _identityManager.RefreshTokenLogin(refreshToken.RefreshToken);
            return Ok(result);
        }

        [HttpPost("refreshPassword")]
        public async Task<IActionResult> RefreshPassword (NewPasswordDto password)
        {
            var userId = User.Identities.First().Name;

            var result = await _identityManager.RefreshPassowrd(userId, password.Password, password.NewPassword);
            return Ok(result);
        }
       
        //[HttpGet("isAuth"),Authorize]
        //public async Task<IActionResult> IsAuth()
        //{
        //    var userId = User.Identities.First().Name;

        //    if (userId == null) //authorize koymadık onun yerine böyle bi kotrnol sagladık
        //        return Ok(new ErrorResult("Başarısız"));
        //    return Ok(new SuccessResult("Başarılı"));
        //}

    }
}
