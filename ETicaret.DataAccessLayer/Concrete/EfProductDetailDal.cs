using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using ETicaretEntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfProductDetailDal : GenericRepositoryy<ProductDetail>, IProductDetailDal
    {
        // BURAYI SİL: private readonly ETicaretContext _context;

        public EfProductDetailDal(ETicaretContext context) : base(context)
        {
            // context zaten base üzerinden protected _context'e atanıyor
        }

        public async Task<ProductDetail> GetByProductIdAsync(int productId)
        {
            return await _context.Set<ProductDetail>()
                                  .FirstOrDefaultAsync(pd => pd.ProductID == productId);
        }
    }
}
