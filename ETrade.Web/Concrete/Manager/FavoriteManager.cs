using ETrade.Entities;
using ETrade.Web.Abstract;
using ETrade.Web.Entities;

namespace ETrade.Web.Concrete.Manager
{
    public class FavoriteManager : IFavoriteService
    {
        private IFavoriteRepository _favoriteRepository;

        public FavoriteManager (IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }
        public void InitializeFavorite(int UserId)
        {
            _favoriteRepository.Create(new Favorite()
            {
                UserId = UserId

            });
        }
        public Favorite GetFavoriteByUserId(int UserId)
        {
            return _favoriteRepository.GetByUserId(UserId);//geliyo
        }

		public void AddToFavorite(int userId, int productId)
		{
		
				var favorite = GetFavoriteByUserId(userId); //geliyo
				if (favorite != null) //null geliyo
				{
					var index = favorite.favoriteItems.FindIndex(i => i.ProductId == productId);

					if (index < 0)
					{
					favorite.favoriteItems.Add(new FavoriteItems()
						{
							ProductId = productId,
							FavoriteId= favorite.Id
						});
					}
					else
					{
						
					}
					_favoriteRepository.Update(favorite);
				}
			}
		

		public void DeleteFromFavorite(int userId, int productId)
		{
			var favorite = GetFavoriteByUserId(userId);
			if(favorite!=null)
			{
				_favoriteRepository.DeleteFromFavorite(favorite.Id,productId);
			}
		}
	}
}
