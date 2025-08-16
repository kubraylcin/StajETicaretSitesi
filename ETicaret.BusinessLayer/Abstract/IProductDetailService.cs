using ETicaret.DtoLayer.ProductDetailDto;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Abstract
{
    public interface IProductDetailService : IGenericService<ProductDetail>
    {
        Task<ProductDetailGetDto> GetByProductIdProductDetailAsync(int id);

    }
}
