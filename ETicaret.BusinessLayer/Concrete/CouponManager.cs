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
    public class CouponManager : ICouponService
    {
        private readonly ICouponDal _couponDal;

        public CouponManager(ICouponDal couponDal)
        {
            _couponDal = couponDal;
        }

        public void TAdd(Coupon entity)
        {
            _couponDal.Add(entity);
        }

        public void TDelete(Coupon entity)
        {
            _couponDal.Delete(entity);
        }

        public Coupon TGetById(int id)
        {
            return _couponDal.GetById(id);
        }

        public List<Coupon> TGetListAll()
        {
            return _couponDal.GetListAll();
        }

        public void TUpdate(Coupon entity)
        {
            _couponDal.Update(entity);
        }
    }
}
