using ETrade.Abstract;
using ETrade.Context;
using ETrade.Entities;
using System.Linq.Expressions;

namespace ETrade.Concrete.EntityFraemwork
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, HomeContext>, ICategoryRepository
    {
        //:base demek implementaldıgımız classa bunu gönder demek
        //EfCoreGenericRepository sınıfının yapıcı metodu benden tcontext tipinde sınıf bekliyo
        //ben homecontexti verdim. homecontextin imzası dbcontext
        public EfCoreCategoryRepository(HomeContext homeContext) : base(homeContext)
        {

        }
    }
}
