using ETicaret.DtoLayer.ProductDto;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(int categoryId);
        Task<ProductGetDto> GetByIdProductAsync(int id);

    }
}
