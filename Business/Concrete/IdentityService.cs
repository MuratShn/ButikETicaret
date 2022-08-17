using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Results.ValidationResult;
using DataAccess.Concrete;
using Entities.Concrete.Identitiy;
using Entities.DTO_s;
using Entities.ViewModel_s;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Business.Concrete
{
    public class IdentityService : IIdentityManager
    {
        //Google Facebook login kısmında kod tekrarı var refactoring yapılmaı

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenManager _tokenManager;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public IdentityService(UserManager<AppUser> userManager, ITokenManager tokenManager, RoleManager<AppRole> roleManager, IConfiguration configuration, HttpClient httpClient)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<IResult> Add(CreateUserVM User)
        {
            var user = new AppUser() //ellen userer nesnesini olusturduk
            {
                Email = User.Email,
                Name = User.Name,
                Surname = User.Surname,
                UserName = User.UserName,
                Gender = User.Gender,
            };

            var userresult = await _userManager.CreateAsync(user, User.Password); //database'e kaydettık sıfreyı ayrı yazdık o sayede haslandi


            if (userresult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer"); //user'a role ekledik
                return new SuccessResult("Başarıyla oluşturuldu");
            }
            else
            {
                return new ErrorResult("Böyle bir kullanıcı bulunmaktadır");
            }
        }

        public async Task<IResult> AddRole(string role)
        {
            var result = await _roleManager.CreateAsync(new() { Name = role });
            if (result.Succeeded)
            {
                return new SuccessResult("Role Başarıyla eklendl");
            }
            return new ErrorResult("Hata");

        }

        public async Task<IResult> GetUserProfile(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var result = new UserDetailDto { Email = user.Email, Gender = user.Gender, Id = user.Id, Name = user.Name, Roles = roles.ToList(), Surname = user.Surname, UserName = user.UserName };

            _userManager.Dispose();

            return new DataResult<UserDetailDto>(result, true);
        }

        public async Task<IResult> Login(UserLoginVM User)
        {
            var user = await _userManager.FindByNameAsync(User.UsernameOrEmail); //kullanıcı adından kontrol ettik

            if (user == null)
                user = await _userManager.FindByEmailAsync(User.UsernameOrEmail); //mailden kontrol ettik

            if (user == null)
                return new ErrorResult("Kullanıcı adı veya şifre hatalı"); //ikiside yoksa hatayı gonderdık

            var result = await _userManager.CheckPasswordAsync(user, User.Password);

            if (result)
            {
                var token = _tokenManager.CreateToken(user);
                await UpdateRefreshToken(user, token.data.RefreshToken, token.data.Expiration); //diğer login işlemlerindede yapılmalı
                return token;
            }
            return new ErrorResult("Hata");

        }

        public async Task<IResult> GoogleLogin(GoogleLoginVm User)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["ExternalLogin:Google-Client-Id"] }
            };
            Payload payload = await GoogleJsonWebSignature.ValidateAsync(User.IdToken, settings);
            UserLoginInfo userLoginInfo = new(User.Provider, payload.Subject, User.Provider);
            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Email = User.Email,
                        Gender = '0',
                        Name = User.FirstName,
                        Surname = User.LastName,
                        UserName = User.Email
                    };
                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, userLoginInfo);
            else
                return new ErrorResult("Hata");

            var token = _tokenManager.CreateToken(user);
            return token;
        }
        public async Task<IResult> RefreshTokenLogin(string refreshToken)
        {
            AppUser user = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                var token = _tokenManager.CreateToken(user);
                await UpdateRefreshToken(user, token.data.RefreshToken, token.data.Expiration);
                return new SuccessDataResult<AccessToken>(token.data, "başarılı");
            }
            return new ErrorResult("Hata");
        }

        public async Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime expretion) //
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = expretion.AddSeconds(15);
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<IResult> RefreshPassowrd(string userId, string password, string newPassword)
        {

            AppUser user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, password, newPassword);

            if (result.Succeeded)
            {
                return new SuccessResult("Şifre Başarıyla Değiştirilmiştir");
            }
            else
            {
                return new ErrorResult("Hata");
            }
        }

        public async Task<IResult> FacebookLogin(GoogleLoginVm User)
        {

            var accessToken = await _httpClient.GetStringAsync("https://graph.facebook.com/oauth/access_token?client_id=517652853462350&client_secret=8a9fa102f1f876d11cee0d4405d958dd&grant_type=client_credentials");

            var access = JsonSerializer.Deserialize<FacebookDto>(accessToken);
            var check = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={User.AuthToken}&access_token={access.AccessToken}");



            var checkResult = JsonSerializer.Deserialize<FacebookResultValidationDto>(check);

            if (checkResult.Data.IsValid)
            {
                string userInfo = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={User.AuthToken}");


                //

                UserLoginInfo userLoginInfo = new("FACEBOOK", checkResult.Data.UserId, "FACEBOOK");

                AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

                bool result = user != null;

                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(User.Email);

                    if (user == null)
                    {
                        user = new()
                        {
                            Email = User.Email,
                            Gender = '0',
                            Name = User.FirstName,
                            Surname = User.LastName,
                            UserName = User.Email
                        };
                        IdentityResult createResult = await _userManager.CreateAsync(user);
                        result = createResult.Succeeded;
                    }
                }

                if (result)
                    await _userManager.AddLoginAsync(user, userLoginInfo);
                else
                    return new ErrorResult("Bu Emaile Sahip Bir Kullanıcı Zaten Bulunmakta ");

                var token = _tokenManager.CreateToken(user);
                return token;

                //
            }

            else
            {
                return new ErrorResult("Giriş yapılamadı");
            }
        }
    }
}
