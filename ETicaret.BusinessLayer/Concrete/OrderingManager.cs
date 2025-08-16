using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class OrderingManager : IOrderingService
    {
        private readonly IOrderingDal _orderingDal;

        public OrderingManager(IOrderingDal orderingDal)
        {
            _orderingDal = orderingDal;
        }

        public void TAdd(Ordering entity) => _orderingDal.Add(entity);

        public void TDelete(Ordering entity) => _orderingDal.Delete(entity);

        public Ordering TGetById(int id) => _orderingDal.GetById(id);

        public List<Ordering> TGetListAll() => _orderingDal.GetListAll();

        public void TUpdate(Ordering entity) => _orderingDal.Update(entity);
    }
}
