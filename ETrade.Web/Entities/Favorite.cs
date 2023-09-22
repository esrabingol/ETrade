using ETrade.Abstract;

namespace ETrade.Web.Entities
{
    public class Favorite : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public List<FavoriteItems> favoriteItems { get; set; }
    }
}
