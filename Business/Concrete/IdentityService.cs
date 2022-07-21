using Business.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Results.ValidationResult;
using DataAccess.Concrete;
using Entities.Concrete.Identitiy;
using Entities.DTO_s;
using Entities.ViewModel_s;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class IdentityService : IIdentityManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenManager _tokenManager;

        public IdentityService(UserManager<AppUser> userManager, ITokenManager tokenManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _roleManager = roleManager;
        }

        public async Task<IResult> Add(CreateUserVM User)
        {
            var user = new AppUser() //ellen üzer nesnesini olusturduk
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
            var result = new UserDetailDto{ Email = user.Email, Gender = user.Gender, Id = user.Id, Name = user.Name, Roles = roles.ToList(), Surname = user.Surname, UserName = user.UserName };

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
                return _tokenManager.CreateToken(user);
            }
            return new ErrorResult("Hata");

        }
    }
}
