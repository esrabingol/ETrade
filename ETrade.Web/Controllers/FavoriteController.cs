using ETrade.Context;
using ETrade.Web.Abstract;
using ETrade.Web.Entities;
using ETrade.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace ETrade.Web.Controllers
{
    public class FavoriteController :Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private HomeContext _context;
        private IFavoriteService _favoriteService;
        public FavoriteController(UserManager<ApplicationUser> userManager, HomeContext context, IFavoriteService favoriteService)
        {
            _userManager = userManager;
            _context = context;
            _favoriteService = favoriteService;

        }
        public IActionResult Index()
        {
            var userIdString = _userManager.GetUserId(User);//geldi

            if (int.TryParse(userIdString, out int UserId)) //geldi
            {
                var favorites = _favoriteService.GetFavoriteByUserId(UserId); //geldi

                if (favorites != null)
                {
                    var favoriteItemsWithProductDetails = _context.FavoriteItems
                        .Include(ci => ci.product)
                        .Where(ci => ci.FavoriteId == favorites.Id)
                        .ToList();


                    var favoriteItems = favoriteItemsWithProductDetails.Select(ci => new FavoriteItemModel
                    {
                        ProductId = ci.ProductId,
                        FavoriteItemId = ci.Id,
                        Name = ci.product.ProductName,
                        Price = (decimal)ci.product.ProductPrice,
                        ImageUrl = ci.product.ImageUrl,
                        Description = ci.product.Description

                    }).ToList();

                    return View(new FavoriteModel
                    {
                        FavoriteId = favorites.Id,
                        favoriteItems = favoriteItems
                    });
                }
                else
                {
                    return View("EmptyCart");
                }
            }
            else
            {
                return View("Error");
            }
         
        }

        public IActionResult AddToFavorite(int productId)
        {
            string userIdString = _userManager.GetUserId(User);
            if(int.TryParse(userIdString, out int userId)) //geliyo
            {
                _favoriteService.AddToFavorite(userId,productId); //geliyo

			}
            else
            {
				// userIdString, geçerli bir tamsayıya çevrilemezse, uygun hata işleme yapılabilir.

			}

            if(!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessageOne"] = "Favorilere Eklemek için giriş yapınız..";
                return RedirectToAction("Login","Home");
            }
            return RedirectToAction("Index");
		}


        public IActionResult DeleteFromFavorite(int productId)
        {
            string UserIdString = _userManager.GetUserId(User);
            if(int.TryParse(UserIdString, out int userId))
            {
                _favoriteService.DeleteFromFavorite(userId,productId);

			}
            else
            {
				// userIdString, geçerli bir tamsayıya çevrilemezse, uygun hata işleme yapılabilir.

			}
			return RedirectToAction("Index");
        }

    }
}
