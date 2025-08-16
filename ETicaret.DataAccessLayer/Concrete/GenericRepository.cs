using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class GenericRepositoryy<T> : IGenericDal<T> where T : class
    {
        protected readonly ETicaretContext _context;

        public GenericRepositoryy(ETicaretContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity); 
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity); // Her metotta Set<T>() çağrısı
            _context.SaveChanges();
        }
    }
}
