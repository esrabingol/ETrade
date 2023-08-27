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

		public void AddToCart(string userId, int productId, int quantity)
		{
            var cart = GetCartByUserId(userId);
            if(cart!=null)
            {
                var index = cart.cartItems.FindIndex(i => i.ProductId == productId);

                if(index<0)
                {
                    cart.cartItems.Add(new CartItem()
                    {
                        ProductId= productId,
                        Quantity = quantity,
                        CartId= cart.Id
                    });
                }
                else
                {
                    cart.cartItems[index].Quantity += quantity; // miktrar güncelleme işlemi 
                }

                _cartRepository.Update(cart);
            }

		}

		public void DeleteBasket(string userId, int productId)
		{
			var cart = GetCartByUserId(userId);
            if(cart!=null) 
            {
                _cartRepository.DeleteFromCart(cart.Id, productId);
            }
		}

		public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetByUserId(userId);
        }


        public void InitializeCart(string userid)
        {
            _cartRepository.Create(new Cart()
            {
                UserId = userid
            });
        }


    }
}
