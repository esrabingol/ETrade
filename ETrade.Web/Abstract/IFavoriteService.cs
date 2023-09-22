using ETrade.Web.Entities;

namespace ETrade.Web.Abstract
{
    public interface IFavoriteService
    {
       void InitializeFavorite(int UserId);
       Favorite GetFavoriteByUserId(int UserId);
	   void AddToFavorite(int userId, int productId);
	   void DeleteFromFavorite(int userId, int productId);
	}
}
