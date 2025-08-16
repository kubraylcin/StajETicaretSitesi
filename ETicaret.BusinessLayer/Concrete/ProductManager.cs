using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DtoLayer.ProductDto;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryDal _categoryDal;

        public ProductManager(IProductDal productDal, ICategoryDal categoryDal)
        {
            _productDal = productDal;
            _categoryDal = categoryDal;
        }

        public async Task<ProductGetDto> GetByIdProductAsync(int id)
        {
            // DAL katmanındaki DTO döndüren metodu kullan
            return await _productDal.GetByIdProductAsync(id);
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var products = await _productDal.GetProductsWithCategoryAsync();

            var result = products.Select(x => new ResultProductWithCategoryDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                CategoryName = x.Category?.CategoryName
            }).ToList();

            return result;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(int categoryId)
        {
            var products = await _productDal.GetProductsWithCategoryByCategoryIdAsync(categoryId);

            var result = products.Select(x => new ResultProductWithCategoryDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                ProductImageUrl = x.ProductImageUrl,
                ProductDescription = x.ProductDescription,
                CategoryId = x.CategoryId,
                CategoryName = x.Category?.CategoryName
            }).ToList();

            return result;
        }

        public void TAdd(Product entity) => _productDal.Add(entity);

        public void TDelete(Product entity) => _productDal.Delete(entity);

        public Product TGetById(int id) => _productDal.GetById(id);

        public List<Product> TGetListAll() => _productDal.GetListAll();

        public void TUpdate(Product entity) => _productDal.Update(entity);
    }
}
