using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Abstract
{
    public interface IFeatureSliderDal : IGenericDal<FeatureSlider>
    {
       
            Task FeatureSliderChangeStatusToTrue(string id);
            Task FeatureSliderChangeStatusToFalse(string id);
        
    }
}
