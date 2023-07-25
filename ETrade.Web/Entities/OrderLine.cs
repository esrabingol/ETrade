using ETrade.Abstract;

namespace ETrade.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order order { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // her producttan kaç tane almış


    }
}
