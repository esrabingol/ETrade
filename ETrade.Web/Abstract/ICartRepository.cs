using ETrade.Abstract;
using ETrade.Entities;

namespace ETrade.Web.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
		void DeleteFromCart(int cartId, int productId);
		Cart GetByUserId(int Id);
        void Update(Cart entity); // veritabanından güncellemek
    }
}
