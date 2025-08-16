using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using ETicaret.DtoLayer.ProductDto;
using ETicaretEntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfProductDal : GenericRepositoryy<Product>, IProductDal
    {
        private readonly ETicaretContext _context;
        public EfProductDal(ETicaretContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductGetDto> GetByIdProductAsync(int id)
        {
            var product = await _context.Products
                                .FirstOrDefaultAsync(x => x.ProductId == id);

            if (product == null)
                return null;

            return new ProductGetDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductImageUrl = product.ProductImageUrl,
                ProductDescription = product.ProductDescription,
                CategoryId = product.CategoryId
            };
        }
    

        public async Task<List<Product>> GetProductsWithCategoryAsync()
        {
            return await _context.Products
                                 .Include(x => x.Category)
                                 .ToListAsync();
        }

        public async Task<List<Product>> GetProductsWithCategoryByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                         .Include(x => x.Category)
                         .Where(x => x.CategoryId == categoryId)
                         .ToListAsync();
        }
    }
}
