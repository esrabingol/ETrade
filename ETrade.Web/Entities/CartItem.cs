using ETrade.Abstract;

namespace ETrade.Entities
{
    public class CartItem :IEntity
    {
        public int Id { get; set; }

        public Product product { get; set; }
        public int ProductId { get; set; }
        public Cart cart { get; set; }
        public int CartId { get; set; }
    }
}
