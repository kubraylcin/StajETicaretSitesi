using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class CargoOperationManager : ICargoOperationService
    {
        private readonly ICargoOperationDal _cargoOperationDal;

        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            _cargoOperationDal = cargoOperationDal;
        }

        public void TAdd(CargoOperation entity)
        {
            _cargoOperationDal.Add(entity);
        }

        public void TDelete(CargoOperation entity)
        {
            _cargoOperationDal.Delete(entity);
        }

        public CargoOperation TGetById(int id)
        {
            return _cargoOperationDal.GetById(id);
        }

        public List<CargoOperation> TGetListAll()
        {
            return _cargoOperationDal.GetListAll();
        }

        public void TUpdate(CargoOperation entity)
        {
            _cargoOperationDal.Update(entity);
        }
    }
}
