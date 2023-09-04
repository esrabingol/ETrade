using ETrade.Abstract;
using System.Collections.Generic;

namespace ETrade.Entities
{
    public class Cart:IEntity //Sepet için eklenmiş kullanıcı id ve ürün id 
    {
        public int Id { get; set; }
        public string UserId { get; set; } = ""; // UserId'yi başlangıçta boş bir string olarak ayarlayın
        public List<CartItem> cartItems
        {
            get { return cartItems;}
        } //cart içinde olan bütün bilgilere ulaşılıcak.

    
    }
}
