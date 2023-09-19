using ETrade.Entities;
using ETrade.Web.Abstract;
using ETrade.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETrade.Web.Concrete.Manager
{
    public class CartManager : ICartService
    {

        private ICartRepository _cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository= cartRepository;
        }

        public void InitializeCart(int UserId)
        {
            _cartRepository.Create(new Cart()
            {
                UserId = UserId  
            });
        }

        
        public Cart GetCartByUserId(int UserId)
        {
            return _cartRepository.GetByUserId(UserId); 
        }

		public void AddToCart(int UserId, int productId, int quantity)
		{
			var cart = GetCartByUserId(UserId);
			if(cart !=null)
			{
				var index = cart.cartItems.FindIndex(i => i.ProductId == productId);
				
				if(index<0)
				{
					cart.cartItems.Add(new CartItem()
					{
						ProductId= productId,
						Quantity = quantity,
						CartId = cart.Id
					});
				}
				else
				{
					cart.cartItems[index].Quantity += quantity;
				}
				_cartRepository.Update(cart);
			}
		}

		public void DeleteFromCart(int userId, int productId)
		{
			var cart = GetCartByUserId(userId);
			if(cart !=null)
			{
				_cartRepository.DeleteFromCart(cart.Id,productId);


			}

		}
	}
}
