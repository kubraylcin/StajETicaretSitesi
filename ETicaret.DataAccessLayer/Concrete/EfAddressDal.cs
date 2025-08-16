using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using ETicaretEntityLayer.Entities;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfAddressDal : GenericRepositoryy<Address>, IAddressDal
    {
        public EfAddressDal(ETicaretContext context) : base(context)
        {
        }
    }
}