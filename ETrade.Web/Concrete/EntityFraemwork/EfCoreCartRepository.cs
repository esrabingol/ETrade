using ETrade.Concrete.EntityFraemwork;
using ETrade.Context;
using ETrade.Entities;
using ETrade.Web.Abstract;
using System.Data.Entity;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETrade.Web.Concrete.EntityFraemwork
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart, HomeContext>,ICartRepository
    {
        private readonly HomeContext _context;
        public EfCoreCartRepository(HomeContext context) : base(context)
        {
            _context = context;
        }

		public override void Update(Cart entity)
		{		   
                _context.Carts.Update(entity);
                _context.SaveChanges();            
		}

		public Cart GetByUserId(int Id)
        {
            return _context.Carts
               .Include("cartItems.Product")
                .FirstOrDefault(c => c.Id == Id);
        }

		public void DeleteFromCart(int cartId, int productId)
		{
			var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);

			if (cartItem != null)
			{
				_context.CartItems.Remove(cartItem);
				_context.SaveChanges();
			}
		}
	}
}
