using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using ETicaretEntityLayer.Entities;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfVendorDal : GenericRepositoryy<Vendor>, IVendorDal
    {
        public EfVendorDal(ETicaretContext context) : base(context)
        {
        }
    }
}
