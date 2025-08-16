using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetailDal;

        // Bağımlılık enjeksiyonu ile veri erişim katmanı alınıyor
        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }

        public void TAdd(CargoDetail entity)
        {
            _cargoDetailDal.Add(entity);
        }

        public void TDelete(CargoDetail entity)
        {
            _cargoDetailDal.Delete(entity);
        }

        public CargoDetail TGetById(int id)
        {
            return _cargoDetailDal.GetById(id);
        }

        public List<CargoDetail> TGetListAll()
        {
            return _cargoDetailDal.GetListAll();
        }

        public void TUpdate(CargoDetail entity)
        {
            _cargoDetailDal.Update(entity);
        }
    }
}
