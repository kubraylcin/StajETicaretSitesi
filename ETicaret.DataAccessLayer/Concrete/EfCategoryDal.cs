using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;

using ETicaretEntityLayer.Entities;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfCategoryDal : GenericRepositoryy<Category>, ICategoryDal
    {
        public EfCategoryDal(ETicaretContext context) : base(context)
        {
        }
    }
}