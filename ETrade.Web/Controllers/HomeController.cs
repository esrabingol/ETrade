using ETrade.Abstract;
using ETrade.Entities;
using ETrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace ETrade.Controllers
{
	public class HomeController : Controller
	{		
        private IProductService _productService;

        public HomeController(IProductService productService)
		{
			_productService = productService;
		}		
		
		public IActionResult Index()
		{
			//veri tabanı baglatısı bu 
			return View();
		}
		public IActionResult Products()
		{
			var products = _productService.GetAll();

            return View(new ProductListModel()
			{
				Products = products
            });
		}
		public IActionResult Privacy()
		{
			return View();
		}
      
        public IActionResult LivingRoom()
		{
			//direct to rooms
			return View();
		}
		public IActionResult BedRoom()
		{
			return View();
		}
		public IActionResult BathRoom()
		{
			return View();
		}
		public IActionResult Kitchen()
		{
			return View();
		}

		public IActionResult Favorites()
		{
			return View();
		}
		public IActionResult Basket()
		{
			return View();
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			return View(new Person());
		}

		[HttpPost]
		[ValidateAntiForgeryToken] //security
		public IActionResult SignUp(Person person)
		{
			if (ModelState.IsValid)
			{
				return View(person);
			}

			return View(person);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View(new Person());
		}

		[HttpPost]
		public IActionResult Login(Person person)//bunlar burda kullanılıyor person silinmez
		{
			if (ModelState.IsValid)
			{
				return View(person);
			}

			return View(person);
		}
	}
}

	

