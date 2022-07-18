using Business.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Results.ValidationResult;
using Entities.Concrete.Identitiy;
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
        
        public IdentityService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IResult Add(CreateUserVM User)
        {
            var userresult = _userManager.CreateAsync(new()
            {
                Email = User.Email,
                Name = User.Name,
                Surname = User.Surname,
                UserName = User.UserName,
                Gender = User.Gender
            }, User.Password);
            
            if (userresult.Result.Succeeded)
            {
                return new SuccessResult("Başarıyla oluşturuldu");
            }
            else
            {
                return new ErrorResult(userresult.Result.Errors.ToString());
            }
        }
    }
}
