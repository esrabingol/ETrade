using ETrade.Abstract;
using ETrade.Context;
using ETrade.Entities;
using ETrade.Models;
using ETrade.Web.Abstract;
using ETrade.Web.Entities;
using ETrade.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ETrade.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ICartService _cartService;
        private readonly HomeContext _context;
        public HomeController(ICartService cartService, IProductService productService,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            HomeContext context)
        {
            _productService = productService;
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
            _context = context;

        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Details(int? id) //ürün detay kısmını ekrana yansıtmak
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _productService.GetById((int)id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Products()
        {
            var products = _productService.GetAll();
            var product = products.FirstOrDefault();

            return View(product);
        }
        public IActionResult Categories()
        {
            // HomeController bize ProductListModel döndürüyor
            return View(new ProductListModel()
            { //productlistmodel ile tüm kategorileri listelememi sağlıyıcam
                Products = _productService.GetAll()
            });

        }


        public IActionResult LivingRoom()
        {


            var allProducts = _productService.GetAll(); // Tüm ürünleri al

            var LProducts = allProducts.Where(p => p.ProductCategory.Contains("Mobilya", StringComparison.OrdinalIgnoreCase)
            || p.ProductCategory.Contains("Ev", StringComparison.OrdinalIgnoreCase)).ToList();

            var model = new ProductListModel()
            {
                Products = LProducts
            };

            return View(model);
        }
        public IActionResult BathRoom()
        {
            //pembe
            var allProducts = _productService.GetAll(); // Tüm ürünleri al

            var BProducts = allProducts.Where(p => p.ProductCategory.Contains("Banyo", StringComparison.OrdinalIgnoreCase)).ToList();

            var model = new ProductListModel()
            {
                Products = BProducts
            };

            return View(model);

        }
        public IActionResult BedRoom()
        {
            var allProducts = _productService.GetAll();

            var BProducts = allProducts.Where(p => p.ProductCategory.Contains("Yatak Odası", StringComparison.OrdinalIgnoreCase)).ToList();

            var model = new ProductListModel()
            {
                Products = BProducts
            };

            return View(model);
        }

        public IActionResult Kitchen()
        {
            var allProducts = _productService.GetAll();
            var KProducts = allProducts.Where(p => p.ProductCategory.Contains("Mutfak", StringComparison.OrdinalIgnoreCase)).ToList();
            var model = new ProductListModel()
            {
                Products = KProducts
            };
            return View(model);
        }

        public IActionResult Favorites()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp() //Register
        {
            return View(new SignUpModel());
        }


        [HttpPost]
        //[ValidateAntiForgeryToken] //security
        public async Task<IActionResult> SignUp(SignUpModel model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser //SignUpModeldeki tüm alanlar kullanılıyor.
            {
                Name = model.FirstName,
                LastName = model.Last_Name,
                Email = model.Email,
                PhoneNumber = model.Phone,

            };
            var result = await _userManager.CreateAsync(user, model.Password); // Veritabanına gönder

            if (!result.Succeeded && result.Errors != null && !result.Errors.Any())
            {
                //loginde hata alırsa hatayu alıp ekrana bastık
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            // Kullanıcı başarıyla oluşturuldu, bir role atanabilir:
            await _userManager.AddToRoleAsync(user, "User");
            // Kullanıcıyı oturum açtırabilirsiniz (isteğe bağlı):
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)//bunlar burda kullanılıyor person silinmez
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.FirstName);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı ile daha önce hesap oluşturulmamış");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.FirstName, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Home", "Index");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya parola yanlış");
            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            //if(string.IsNullOrEmpty(Email))
            //   {
            //	return View();
            //}

            //var user = await _userManager.FindByEmailAsync(Email);

            //if(user == null)
            //{
            //	return View();
            //}

            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callback = Url.Action("ResetPassword", "Home", new
            //{
            //	UserId = user.Id,
            //	token=code
            //});

            ////send email kısmını eklemedim 
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult Basket()

        {

            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            if (cart == null)
            {
                cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));//Eğer sepet yoksa oluşturabilirsiniz
            }
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.cartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.product.ProductName,
                    Price = (decimal)i.product.ProductPrice,
                    ImageUrl = i.product.ImageUrl,
                    Quantity = i.Quantity,
                    Description = i.product.Description

                }).ToList()


            });

        }
        [HttpPost]
        public IActionResult DeleteBasket(int productId)
        {
            _cartService.DeleteBasket(_userManager.GetUserId(User), productId);
            return RedirectToAction("Basket", "Home");
        }


        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                var cartItem = new CartItem
                {
                    ProductId = product.ProductId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Basket");
        }

    }
}



