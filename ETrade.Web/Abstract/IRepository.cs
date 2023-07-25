using System.Linq.Expressions;

namespace ETrade.Abstract
{
    public interface IRepository<T> where T : IEntity
    {
        T GetById(int id);
        T GetOne(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Create(T entity);//senin artık add metodun yok create metodun var
        void Update(T entity);
        void Delete(T entity);
    }
}
