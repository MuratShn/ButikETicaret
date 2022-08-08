using Core.Utilities.Results;
using Entities.ViewModel_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IIdentityManager
    {
        Task<IResult> Add(CreateUserVM User);
        Task<IResult> Login(UserLoginVM User);
        Task<IResult> GoogleLogin(GoogleLoginVm User);
        Task<IResult> AddRole(string role);

        Task<IResult> GetUserProfile(string userId);
    }
}
