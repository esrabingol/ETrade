using ETrade.Abstract;
using ETrade.Context;
using ETrade.Entities;
using System.Linq.Expressions;

namespace ETrade.Concrete.EntityFraemwork
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, HomeContext>, IProductRepository
    {
        public EfCoreProductRepository(HomeContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetPopulerProducts()
        {
            throw new NotImplementedException();
        }
    }
}
