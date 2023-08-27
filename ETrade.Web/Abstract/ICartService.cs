using ETrade.Entities;
using ETrade.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETrade.Web.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);

        void AddToCart(string userId, int productId, int quantity);
		void DeleteBasket(string userId, int productId);
	}
}
