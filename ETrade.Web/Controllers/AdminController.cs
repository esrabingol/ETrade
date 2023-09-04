using ETrade.Abstract;
using ETrade.Entities;
using ETrade.Models;
using ETrade.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ETrade.Controllers
{
    //Yönetici Sayfaları
	public class AdminController : Controller
	{		
        private IProductService _productService;
        //private ICategoryService _categoryService;
        public AdminController(IProductService productService/*, ICategoryService categoryService*/)
        {
			_productService = productService;
            //_categoryService = categoryService;
        }		
		
		public IActionResult Index()
		{

            return View(new ProductListModel()
            {
                Products = _productService.GetAll()

            });
        }

        [HttpGet]
        public IActionResult CreateProduct() 
        {
            //admin ürün ekleme sayfasını görüntüleyecek
            return View(new ProductModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model, IFormFile file)
        {

			if (model == null || string.IsNullOrWhiteSpace(model.ProductName) || model.ProductPrice <= 0 || string.IsNullOrWhiteSpace(model.Description) || file == null || file.Length <= 0)
			{
				ModelState.AddModelError("", "Lütfen tüm alanları doldurun.");
				return View(model);
			}
			if (file != null && file.Length > 0)
                {
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/templates/images", file.FileName);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); //dosya asenkron bir şekilde kayıt edilicek
                    }
                    var entity = new Product()
                    {
                        ProductName = model.ProductName,
                        ProductPrice = model.ProductPrice,
                        Description = model.Description,
                        ProductCategory= model.ProductCategory,
                        ImageUrl = model.ImageUrl,
                        UserId = model.UserId
                    };
                    entity.ImageUrl = "templates/images/" + file.FileName;
                    _productService.Create(entity);


                    return RedirectToAction("Index");
                }
            
                return View(model);
            
        }
        public IActionResult EditProduct(int? id) //id'ye uygun bilgi form ekranına gönderilir
        {
            if(id== null) //id yok ise hata mesajı
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if (entity==null) //entity yok ise hata mesajı
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                ProductPrice = entity.ProductPrice,
                Description =entity.Description,
                ProductCategory=entity.ProductCategory,
                ImageUrl = entity.ImageUrl,
                UserId=entity.UserId //Personelin kim olduğu belli oluyor.
                
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _productService.GetById(model.ProductId);
                if (entity == null)
                {
                    return NotFound();
                }

			
				if (!string.IsNullOrEmpty(model.ProductName))
				{
					entity.ProductName = model.ProductName;
				}

				if (!string.IsNullOrEmpty(model.ProductCategory))
				{
					entity.ProductCategory = model.ProductCategory;
				}

				if (model.ProductPrice.HasValue)
				{
					entity.ProductPrice = model.ProductPrice.Value;
				}

				if (!string.IsNullOrEmpty(model.Description))
				{
					entity.Description = model.Description;
				}

				if (file != null && file.Length > 0)
                {
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/templates/images", file.FileName);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); //dosya asenkron bir şekilde kayıt edilicek
                    }

                    entity.ImageUrl = "templates/images/" + file.FileName;
                }
                _productService.Update(entity); // entity göre ürün güncellemeyi yapar
                return RedirectToAction("Index", "Admin");
            }

            return View(model);


        }



		[HttpPost]
        public IActionResult DeleteProduct(int ProductId)
        {
            var entity = _productService.GetById(ProductId);
            if(entity != null) // db de uygun bir id ile eşleşmesi durumu
            {   
                _productService.Delete(entity);
            

            }
            return RedirectToAction("Index");
           
        }


      
        //public IActionResult CategoryList() //Ürüne uygun kategori
        //{
        //    return View(new CategoryListModel()
        //    {
        //        Categories = _categoryService.GetAll()
        //    });

        //}

    }
}

	

