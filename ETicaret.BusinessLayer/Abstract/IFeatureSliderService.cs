using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Abstract
{
    public interface IFeatureSliderService : IGenericService<FeatureSlider>
    {
        Task TFeatureSliderChangeStatusToTrue(string id);
        Task TFeatureSliderChangeStatusToFalse(string id);
    }
}
