using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AddressService : IAddressManager
    {
        private readonly IAddressDal _addressDal;
        public AddressService(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        public IResult AddAddress(int userId, Address Entitiy)
        {
            if (!(Entitiy.UserId == userId))
            {
                Entitiy.UserId = userId;
            }
            _addressDal.Add(Entitiy);
            return new SuccessResult("Adres Ekleme Başarılı");

        }

        public IResult deleteAddress(int AddressId, int userId)
        {
            var entity = _addressDal.Get(x => x.Id == AddressId && x.UserId == userId);
            if (entity is not null)
            {
                _addressDal.Delete(entity);
                return new SuccessResult("Adres Silinmiştir");
            }
            return new ErrorResult("Hata");

        }

        public IResult getAddressById(int AddressId)
        {
            throw new NotImplementedException();
        }

        public IResult getAllAddres(int userId)
        {
            var result = _addressDal.GetAll(x => x.UserId == userId);
            return new SuccessDataResult<List<Address>>(result);
        }
    }
}
