
using ETrade.Abstract;
using ETrade.Context;
using ETrade.Entities;

namespace ETrade.Concrete.EntityFraemwork
{
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order, HomeContext>, IOrderRepository
    {
        public EfCoreOrderRepository(HomeContext context) : base(context)
        {
        }
    }
}
