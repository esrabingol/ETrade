using ETrade.Abstract;
using ETrade.Context;
using ETrade.Entities;

namespace ETrade.Concrete.EntityFraemwork
{
    public class EfCoreAnnouncementRepository : EfCoreGenericRepository<Announcement, HomeContext>, IAnnouncementRepository
    {
        public EfCoreAnnouncementRepository(HomeContext homeContext):base(homeContext)
        {

        }
    }
}
