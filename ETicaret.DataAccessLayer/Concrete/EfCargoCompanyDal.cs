using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using ETicaretEntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfCargoCompanyDal : GenericRepositoryy<CargoCompany>, ICargoCompanyDal
    {
    public EfCargoCompanyDal(ETicaretContext context) : base(context)
        {
        }
    }

}
