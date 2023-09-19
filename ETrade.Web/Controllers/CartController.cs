using ETrade.Context;
using ETrade.Entities;
using ETrade.Web.Abstract;
using ETrade.Web.Entities;
using ETrade.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Data.Entity;

namespace ETrade.Controllers
{
	[Authorize]
    public class CartController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ICartService _cartService;
        private readonly HomeContext _context; // veritabanı işlemleri
        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager,HomeContext context)
        {

            _userManager = userManager;
            _cartService = cartService;
            _context = context;

        }

		public IActionResult Index()
		{
			var userIdString = _userManager.GetUserId(User);

			if (int.TryParse(userIdString, out int userId))
			{
				var cart = _cartService.GetCartByUserId(userId);

				if (cart != null) 
				{
					// CartItems koleksiyonunu yüklerken ilişkili Product bilgilerini de yükleyin
					var cartItemsWithProductDetails = _context.CartItems
						.Include(ci => ci.product) // Product tablosunu yükleyin
						.Where(ci => ci.CartId == cart.Id)
						.ToList();

					var cartItems = cartItemsWithProductDetails.Select(ci => new CartItemModel
					{
						ProductId = ci.ProductId,
						CartItemId = ci.Id,
						Name = ci.product.ProductName,
						Price = (decimal)ci.product.ProductPrice,
						ImageUrl = ci.product.ImageUrl,
						Description = ci.product.Description,
						Quantity = ci.Quantity
					}).ToList();

					return View(new CartModel
					{
						CartId = cart.Id, 
						CartItems = cartItems
					});
				}
				else
				{
					// Sepet bulunamadı veya boş, uygun bir işlem yapın
					return View("EmptyCart");
				}
			}
			else
			{
				// Kullanıcı kimliği dönüştürülemedi, hata işleme kodu ekleyin veya kullanıcıyı bir hata sayfasına yönlendirin.
				return View("Error");
			}
		}


		[HttpPost]
		public IActionResult AddToCart(int productId, int quantity)
		{
			string userIdString = _userManager.GetUserId(User);
			if (int.TryParse(userIdString, out int userId))
			{
				_cartService.AddToCart(userId, productId, quantity); // geldi
			}
			else
			{
				// userIdString, geçerli bir tamsayıya çevrilemezse, uygun hata işleme yapılabilir.
			}

			if (!User.Identity.IsAuthenticated)
			{
				TempData["ErrorMessageOne"] = "Sepete Eklemek için giriş yapınız.";
				return RedirectToAction("Login", "Home"); // Kullanıcıyı giriş sayfasına yönlendirin.
			}

			return RedirectToAction("Index");

			
			
		}

		[HttpPost]
		public IActionResult DeleteFromCart(int productId)
		{
			string userIdString = _userManager.GetUserId(User);
			if (int.TryParse(userIdString, out int userId))
			{
				_cartService.DeleteFromCart(userId, productId); 
			}
			else
			{
				// userIdString, geçerli bir tamsayıya çevrilemezse, uygun hata işleme yapılabilir.
			}
			return RedirectToAction("Index");
		}







	}
}

