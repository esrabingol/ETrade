using ETrade.Abstract;
using System.Collections.Generic;

namespace ETrade.Entities
{
    public class Cart:IEntity //Sepet için eklenmiş kullanıcı id ve ürün id 
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public List<CartItem> cartItems { get; set; }
    
    }
}
