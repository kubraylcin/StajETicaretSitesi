using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using ETicaretEntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfProductImageDal : GenericRepositoryy<ProductImage>, IProductImageDal
    {
        private readonly ETicaretContext _context;
        public EfProductImageDal(ETicaretContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductImage> GetByProductIdProductImageAsync(int id)
        {
            return await _context.ProductImages
                                  .Where(x => x.ProductID == id)
                                  .FirstOrDefaultAsync();
        }
    }
}
