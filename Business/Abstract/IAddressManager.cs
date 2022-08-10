using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAddressManager
    {
        IResult AddAddress(int userId,Address Entitiy);
        IResult getAllAddres(int userId);
        IResult getAddressById(int AddressId);
        IResult deleteAddress(int AddressId, int userId);
    }
}
