using ETrade.Abstract;
using ETrade.Web.Entities;

namespace ETrade.Web.Abstract
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
		void DeleteFromFavorite(int favoriteId, int productId);
		Favorite GetByUserId(int UserId);
    }
}
