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
    public class SpecialOfferManager : ISpecialOfferService
    {
        private readonly ISpecialOfferDal _specialOfferDal;

        public SpecialOfferManager(ISpecialOfferDal specialOfferDal)
        {
            _specialOfferDal = specialOfferDal;
        }

        public void TAdd(SpecialOffer entity)
        {
            _specialOfferDal.Add(entity);
        }

        public void TDelete(SpecialOffer entity)
        {
            _specialOfferDal.Delete(entity);
        }

        public SpecialOffer TGetById(int id)
        {
            return _specialOfferDal.GetById(id);
        }

        public List<SpecialOffer> TGetListAll()
        {
            return _specialOfferDal.GetListAll();
        }

        public void TUpdate(SpecialOffer entity)
        {
            _specialOfferDal.Update(entity);
        }
    }
}
