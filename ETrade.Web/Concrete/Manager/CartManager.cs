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

		public void AddToCart(int Id, int productId, int quantity)
		{
            var cart = GetCartByUserId(Id);
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

		public void DeleteBasket(int Id, int productId)
		{
			var cart = GetCartByUserId(Id);
            if(cart!=null) 
            {
                _cartRepository.DeleteFromCart(cart.Id, productId);
            }
		}

		public Cart GetCartByUserId(int Id)
        {
            return _cartRepository.GetByUserId(Id); //kullancının id'si
        }


        public void InitializeCart(int Id) //kullancı Id alınıp string Id formatına dönüştürülüp kaydedilicek
        {
            _cartRepository.Create(new Cart()
            {
                UserId = Id.ToString()   //userid gelmiyor?
            });
        }


    }
}
