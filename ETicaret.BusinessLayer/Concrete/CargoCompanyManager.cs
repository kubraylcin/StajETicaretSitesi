using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class CargoCompanyManager : ICargoCompanyService
    {
        private readonly ICargoCompanyDal _cargoCompanyDal;

        // Constructor ile bağımlılık enjekte edildi
        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }

        public void TAdd(CargoCompany entity)
        {
            _cargoCompanyDal.Add(entity);
        }

        public void TDelete(CargoCompany entity)
        {
            _cargoCompanyDal.Delete(entity);
        }

        public CargoCompany TGetById(int id)
        {
            return _cargoCompanyDal.GetById(id);
        }

        public List<CargoCompany> TGetListAll()
        {
            return _cargoCompanyDal.GetListAll();
        }

        public void TUpdate(CargoCompany entity)
        {
            _cargoCompanyDal.Update(entity);
        }
    }
}
