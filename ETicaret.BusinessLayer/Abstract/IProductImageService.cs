using ETicaret.DtoLayer.ProductImageDto;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Abstract
{
    public interface IProductImageService : IGenericService<ProductImage>
    {
        Task<ProductImageGetDto> GetByProductIdProductImageAsync(int id);
    }
}
