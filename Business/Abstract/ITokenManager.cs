using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete.Identitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITokenManager
    {
        IDataResult<AccessToken> CreateToken(AppUser user );
    }
}
