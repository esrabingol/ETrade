using ETrade.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETrade.Concrete.EntityFraemwork
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T>
        where T : IEntity
        where TContext : DbContext //burada tcontext dbcontext imzası olan her sınıfı kabul eder
    {
        protected TContext _context; //burayı çağıran kimse contextide o versin dedik

        //zate tcontext null olamaz çağıran göndermek zorunda 
        public EfCoreGenericRepository(TContext context)
        {
            _context = context;
        }

        public virtual void Create(T entity)
        {
            //hepsini buna benzetirsin sen
            _context.Set<T>().Add(entity); //aldıgımız contexti kullandık, 
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? _context.Set<T>().ToList()
                : _context.Set<T>().Where(filter).ToList();


        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual T GetOne(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).SingleOrDefault();
        }

        public virtual void Update(T entity) //(virtual) override edebilirim demek.
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges(); //entity'nin değişen kısımları direkt update edilir.
        }
    }
}
