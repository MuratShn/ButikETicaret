using Business.Abstract;
using Core.Caching;
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
        private readonly ICacheManager _cacheManager;

        public UsersController(IIdentityManager identityManager, ICacheManager cacheManager)
        {
            _identityManager = identityManager;
            _cacheManager = cacheManager;
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
            _cacheManager.RemoveByPattern("UserService.GetUser");

            var result = await _identityManager.Login(user);
            return Ok(result);
        }

        [HttpPost("externalLogin")]
        public async Task<IActionResult> ExternalLogin(GoogleLoginVm User)
        {
            _cacheManager.RemoveByPattern("UserService.GetUser");

            var result = await _identityManager.ExternalLogin(User);
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
            if (_cacheManager.IsAdd("UserService.GetUser"))
            {
                return  Ok(_cacheManager.Get<IResult>("UserService.GetUser"));
            }

            var userId = User.Identities.First().Name;

            if (userId == null) //authorize koymadık onun yerine böyle bi kotrnol sagladık
                return Ok(new ErrorResult("Giriş yapılmamıştır"));
                //return BadRequest("Giriş yapılmamıştır");

            var result = await _identityManager.GetUserProfile(userId);

            _cacheManager.Add("UserService.GetUser", result);
            return Ok(result);
        }

        [HttpPost("refreshTokenLogin")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] AccessToken refreshToken)
        {
            var result = await _identityManager.RefreshTokenLogin(refreshToken.RefreshToken);
            return Ok(result);
        }

        [HttpPost("refreshPassword")]
        [Authorize]

        public async Task<IActionResult> RefreshPassword (NewPasswordDto password)
        {
            var userId = User.Identities.First().Name;

            var result = await _identityManager.RefreshPassowrd(userId, password.Password, password.NewPassword);
            return Ok(result);
        }
      
        [HttpPost("refreshUserInfo")]
        [Authorize]
        public async Task<IActionResult> RefreshUserİnfo(CreateUserVM newUser)
        {
            var result = await _identityManager.RefreshUserInfo(newUser, User.Identities.First().Name);
            return Ok(result);
        }

    

    }
}
