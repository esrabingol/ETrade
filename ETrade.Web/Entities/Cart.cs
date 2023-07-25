using ETrade.Abstract;

namespace ETrade.Entities
{
    public class Cart:IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> cartItems { get; set; }
    }
}
