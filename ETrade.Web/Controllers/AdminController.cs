using ETrade.Abstract;
using ETrade.Entities;
using ETrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace ETrade.Controllers
{
	public class AdminController : Controller
	{		
        private IProductService _productService;

        public AdminController(IProductService productService)
		{
			_productService = productService;
		}		
		
		public IActionResult Index()
		{
			//veri tabanı baglatısı bu 
			return View();
		}

        public IActionResult CreateProduct()
        {
            //admin ürün ekleme sayfasını görüntüleyecek
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            //admin ekrandan ürünleri girip kaydet butonuna düştügünde 
            //_productRespo.Create(product) diye ürünü eklicen
            return View();
        }
    }
}

	

