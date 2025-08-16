using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Concrete
{
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        public void TAdd(Address entity)
        {
            _addressDal.Add(entity);
        }

        public void TDelete(Address entity)
        {
            _addressDal.Delete(entity);
        }

        public Address TGetById(int id)
        {
            return _addressDal.GetById(id);
        }

        public List<Address> TGetListAll()
        {
            return _addressDal.GetListAll();
        }

        public void TUpdate(Address entity)
        {
            _addressDal.Update(entity);
        }
    }
}
