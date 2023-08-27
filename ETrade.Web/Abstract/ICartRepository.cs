using ETrade.Abstract;
using ETrade.Entities;

namespace ETrade.Web.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
		void DeleteFromCart(int cartId, int productId);
		Cart GetByUserId(string userId);
        void Update(Cart entity);
    }
}
