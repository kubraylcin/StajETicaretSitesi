using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Abstract
{
    public interface IProductDetailDal: IGenericDal<ProductDetail>
    {
        Task<ProductDetail> GetByProductIdAsync(int productId);

    }
}
