using ETrade.Entities;
using ETrade.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETrade.Web.Abstract
{
    public interface ICartService
    {
        void InitializeCart(int UserId); //amaç alışveriş sepeti kurmak
        Cart GetCartByUserId(int UserId);
        void AddToCart(int UserId, int productId, int quantity);
		void DeleteFromCart(int userId, int productId);
	}
}
