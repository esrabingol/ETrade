using ETrade.Abstract;
using ETrade.Entities;

namespace ETrade.Web.Entities
{
    public class FavoriteItems : IEntity
    {
        public int Id { get; set; }

        public Product product { get; set; }
        public int ProductId { get; set; }

        public Favorite favorite { get; set; }
        public int FavoriteId { get; set; }

    }
}
