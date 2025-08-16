using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class CargoCustomerManager : ICargoCustomerService
    {
        private readonly ICargoCustomerDal _cargoCustomerDal;

        // Constructor ile bağımlılık enjekte edildi
        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
        {
            _cargoCustomerDal = cargoCustomerDal;
        }

        public void TAdd(CargoCustomer entity)
        {
            _cargoCustomerDal.Add(entity);
        }

        public void TDelete(CargoCustomer entity)
        {
            _cargoCustomerDal.Delete(entity);
        }

        public CargoCustomer TGetById(int id)
        {
            return _cargoCustomerDal.GetById(id);
        }

        public List<CargoCustomer> TGetListAll()
        {
            return _cargoCustomerDal.GetListAll();
        }

        public void TUpdate(CargoCustomer entity)
        {
            _cargoCustomerDal.Update(entity);
        }
    }
}
