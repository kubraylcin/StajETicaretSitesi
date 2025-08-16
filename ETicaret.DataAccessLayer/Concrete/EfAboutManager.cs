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
    public class EfAboutManager : GenericRepositoryy<About>,IAboutDal
    {
        public EfAboutManager(ETicaretContext context) : base(context)
        {
        }
    }
}
