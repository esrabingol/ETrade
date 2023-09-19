using ETrade.Abstract;
using ETrade.Entities;

namespace ETrade.Web.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetByUserId(int UserId);
        void DeleteFromCart(int cartId, int productId);

    }
}
