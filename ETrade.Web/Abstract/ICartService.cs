using ETrade.Entities;
using ETrade.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETrade.Web.Abstract
{
    public interface ICartService
    {
        void InitializeCart(int Id); //bizim için bir asnetusersdaki Id
        Cart GetCartByUserId(int Id); //kullanıcı login olunca bize gelicek

        void AddToCart(int Id, int productId, int quantity);
		void DeleteBasket(int Id, int productId);
	}
}
