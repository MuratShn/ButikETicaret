using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TokenService : ITokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public TokenService(IConfiguration configuration, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IDataResult<AccessToken> CreateToken(AppUser user)
        {
            AccessToken token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(15);



            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials:signingCredentials,
                claims: SetClaims(user).Result.ToList()
                );
            JwtSecurityTokenHandler tokenHandler = new();
            
            token.Token= tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();

            return new DataResult<AccessToken>(token, "Başarılı",true);
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private async Task<IEnumerable<Claim>> SetClaims(AppUser user)
        {
            //Olusturulucak jwt'deki kişinin bilgileri
            var claims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);
            
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            
            return  claims;
        }
    }
}

//ValidAudience = Configuration["Token:Audience"],
//ValidIssuer = Configuration["Token:Issuer"],
//IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]))