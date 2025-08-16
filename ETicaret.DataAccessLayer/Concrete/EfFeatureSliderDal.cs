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
    public class EfFeatureSliderDal : GenericRepositoryy<FeatureSlider>, IFeatureSliderDal
    {
        private readonly ETicaretContext _context;

        public EfFeatureSliderDal(ETicaretContext context):base(context) 
        {
            _context = context;
        }

        public async Task FeatureSliderChangeStatusToFalse(string id)
        {
            var featureSlider = _context.FeatureSliders.Find(int.Parse(id));
            if (featureSlider != null)
            {
                featureSlider.Status = true;
                _context.Update(featureSlider);
                await _context.SaveChangesAsync();
            }
        }

        public  async Task FeatureSliderChangeStatusToTrue(string id)
        {
            var featureSlider = _context.FeatureSliders.Find(int.Parse(id));
            if (featureSlider != null)
            {
                featureSlider.Status = false;
                _context.Update(featureSlider);
                await _context.SaveChangesAsync();
            }
        }
    }
}
