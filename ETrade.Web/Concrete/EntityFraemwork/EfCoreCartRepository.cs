using ETrade.Concrete.EntityFraemwork;
using ETrade.Context;
using ETrade.Entities;
using ETrade.Web.Abstract;
using Microsoft.EntityFrameworkCore;



namespace ETrade.Web.Concrete.EntityFraemwork
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart, HomeContext>,ICartRepository
    {
		private readonly HomeContext _context;
		public EfCoreCartRepository(HomeContext context) : base(context)
		{
			_context = context;
		}

		public Cart GetByUserId(int UserId) //userid sadece geliyor
		{
			return _context.Carts
			   .Include(c => c.cartItems) // cartItems'i Include edin
			   .ThenInclude(ci => ci.product) // cartItems'in içindeki Product'u Include edin
			   .FirstOrDefault(i => i.UserId == UserId);
		}


		public override void Update(Cart entity)
		{

			_context.Carts.Update(entity);
			_context.SaveChanges();

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
