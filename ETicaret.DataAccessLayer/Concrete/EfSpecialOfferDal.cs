using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfSpecialOfferDal : GenericRepositoryy<SpecialOffer>, ISpecialOfferDal
    {
        public EfSpecialOfferDal(ETicaretContext context) : base(context)
        {
        }
    }
}
