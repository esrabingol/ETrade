using ETrade.Abstract;

namespace ETrade.Entities
{
    public class CartItem :IEntity //ürün id ve sepet id 
    {
        public int Id { get; set; }

        public Product product { get; set; }
        public int ProductId { get; set; }
        public Cart cart { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }
}
