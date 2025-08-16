using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class VendorManager : IVendorService
    {
        private readonly IVendorDal _vendorDal;

        public VendorManager(IVendorDal vendorDal)
        {
            _vendorDal = vendorDal;
        }

        public void TAdd(Vendor entity)
        {
            _vendorDal.Add(entity);
        }

        public void TDelete(Vendor entity)
        {
            _vendorDal.Delete(entity);
        }

        public Vendor TGetById(int id)
        {
            return _vendorDal.GetById(id);
        }

        public List<Vendor> TGetListAll()
        {
            return _vendorDal.GetListAll();
        }

        public void TUpdate(Vendor entity)
        {
            _vendorDal.Update(entity);
        }
    }
}
