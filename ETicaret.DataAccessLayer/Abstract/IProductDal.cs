using ETicaret.DtoLayer.ProductDto;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Abstract
{
    public interface IProductDal: IGenericDal<Product>
    {
        Task<List<Product>> GetProductsWithCategoryAsync();
        Task<List<Product>> GetProductsWithCategoryByCategoryIdAsync(int categoryId);
        Task<ProductGetDto> GetByIdProductAsync(int id);
        
    }
}
