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
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IFeatureSliderDal _featureSliderDal;

        public FeatureSliderService(IFeatureSliderDal featureSliderDal)
        {
            _featureSliderDal = featureSliderDal;
        }

        public void TAdd(FeatureSlider entity)
        {
            _featureSliderDal.Add(entity);
        }

        public void TDelete(FeatureSlider entity)
        {
            _featureSliderDal.Delete(entity);
        }

        public Task TFeatureSliderChangeStatusToFalse(string id)
        {
            return _featureSliderDal.FeatureSliderChangeStatusToFalse(id);
        }

        public Task TFeatureSliderChangeStatusToTrue(string id)
        {
            return _featureSliderDal.FeatureSliderChangeStatusToTrue(id);
        }

        public FeatureSlider TGetById(int id)
        {
            return _featureSliderDal.GetById(id);
        }

        public List<FeatureSlider> TGetListAll()
        {
            return _featureSliderDal.GetListAll();
        }

        public void TUpdate(FeatureSlider entity)
        {
            _featureSliderDal.Update(entity);
        }
    }
}
