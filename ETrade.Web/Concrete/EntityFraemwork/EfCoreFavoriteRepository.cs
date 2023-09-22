using ETrade.Concrete.EntityFraemwork;
using ETrade.Context;
using ETrade.Entities;
using ETrade.Web.Abstract;
using ETrade.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Web.Concrete.EntityFraemwork
{
	public class EfCoreFavoriteRepository : EfCoreGenericRepository<Favorite, HomeContext>, IFavoriteRepository
	{
		private  HomeContext _context;
		public EfCoreFavoriteRepository(HomeContext context) : base(context)
		{
			_context = context;
		}



		public override void Update(Favorite entity)
		{

			_context.Favorites.Update(entity);
			_context.SaveChanges();

		}

		public void DeleteFromFavorite(int favoriteId, int productId)
		{
			var FavoriteItem = _context.FavoriteItems.FirstOrDefault(ci => ci.FavoriteId == favoriteId && ci.ProductId == productId);
			if(FavoriteItem != null)
			{
				_context.FavoriteItems.Remove(FavoriteItem);
				_context.SaveChanges();
			}

		}

		public Favorite GetByUserId(int UserId) //ok
		{
			return _context.Favorites
				.Include(c=> c.favoriteItems)
				.ThenInclude(ci => ci.product)
				.FirstOrDefault(i =>i.UserId == UserId); //geliyo
		}
	}
}
