using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete.Identitiy;
using Entities.ViewModel_s;
using System;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IIdentityManager
    {
        Task<IResult> Add(CreateUserVM User);
        Task<IResult> Login(UserLoginVM User);
        Task<IResult> ExternalLogin(GoogleLoginVm User);

        Task<IResult> AddRole(string role);
        Task UpdateRefreshToken(AppUser user,string refreshToken,DateTime expretion);
        Task<IResult> RefreshTokenLogin(string refreshToken);
        Task<IResult> GetUserProfile(string userId);
        Task<IResult> RefreshPassowrd (string userId,string password,string newPassword);
    }
}
