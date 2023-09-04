using ETrade.Abstract;
using ETrade.Context;
using ETrade.Entities;
using ETrade.Models;
using ETrade.Web.Abstract;
using ETrade.Web.Entities;
using ETrade.Web.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq;
using System.Security.Cryptography;
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

        public IActionResult Index(string message = null)
        {
            ViewBag.Message = message;
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
        public IActionResult LivingRoom(int page = 1)
        {


            var allProducts = _productService.GetAll(); // Tüm ürünleri al

            var LProducts = allProducts.Where(p => p.ProductCategory.Contains("Mobilya", StringComparison.OrdinalIgnoreCase)
            || p.ProductCategory.Contains("Ev", StringComparison.OrdinalIgnoreCase)).ToList();

            int pageSize = 3; // Her sayfada gösterilecek ürün sayısı
       

            var model = new ProductListModel()
            {
                Products = LProducts.Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList(),
                PageNumber = page,
                TotalPages = (int)Math.Ceiling((double)LProducts.Count / pageSize)
            };

            return View(model);
        }
        public IActionResult BathRoom(int page =1)
        {
            
            var allProducts = _productService.GetAll(); // Tüm ürünleri al

            var BProducts = allProducts.Where(p => p.ProductCategory.Contains("Banyo", StringComparison.OrdinalIgnoreCase)).ToList();

            int pageSize = 3;
            var model = new ProductListModel()
            {
                Products = BProducts.Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToList(),
                PageNumber = page,
                TotalPages= (int)Math.Ceiling((double)BProducts.Count/ pageSize)
            };

            return View(model);

        }
        public IActionResult BedRoom(int page =1)
        {
            var allProducts = _productService.GetAll();

            var BProducts = allProducts.Where(p => p.ProductCategory.Contains("Yatak Odası", StringComparison.OrdinalIgnoreCase)).ToList();

            int pageSize = 3;
            var model = new ProductListModel()
            {
                Products = BProducts.Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToList(),
                PageNumber=page,
                TotalPages= (int)Math.Ceiling((double) BProducts.Count/pageSize)
            };

            return View(model);
        }

        public IActionResult Kitchen(int page = 1)
        {
            var allProducts = _productService.GetAll();
            var KProducts = allProducts.Where(p => p.ProductCategory.Contains("Mutfak", StringComparison.OrdinalIgnoreCase)).ToList();

            int pageSize = 3; // Her sayfada gösterilecek ürün sayısı

            var model = new ProductListModel()
            {
                Products = KProducts.Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList(),
                PageNumber = page,
                TotalPages = (int)Math.Ceiling((double)KProducts.Count / pageSize)
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
                UserName =model.Email,
                Password = model.Password
               
            };
            var result = await _userManager.CreateAsync(user, model.Password); // Veritabanına gönder

            if (!result.Succeeded)
            {
                //loginde hata alırsa hatayı alıp ekrana bastık
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            if(result.Succeeded)
            {
               
                // Kullanıcıyı oturum açtırabilirsiniz (isteğe bağlı):
                await _signInManager.SignInAsync(user, isPersistent: false); //kullanıcı bilgileri doğruca geliyor

                // Başarılı kayıt uyarısı ekleyin
                TempData["SuccessMessage"] = "Kaydınız başarıyla oluşturuldu! Lütfen giriş yapın.";

                return RedirectToAction("Login", "Home");
             
            }

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
				TempData["ErrorMessage"] = "Mevcut Hesap Bulunamadı! Bilgilerinizi Kontrol Ediniz.";
				return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.FirstName, model.Password, false, false);

            if (result.Succeeded)
            {
                _cartService.InitializeCart(user.Id);
          
                return RedirectToAction("Index", "Home");
            }
            else
            {
				TempData["ErrorMessage"] = "Kullancı Adı veya Parola Yanlış!";
			}

            
            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Message"] = "Çıkış yapıldı! Devam etmek için giriş yapınız..";
            return RedirectToAction("Index", "Home", new { message = TempData["Message"] });

        }


        public IActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid) // Model geçerli mi kontrolü yapılıyor
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                    if (result.Succeeded)
                    {
                        // Veritabanındaki şifreyi güncelle
   
                        var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                        user.PasswordHash = newPasswordHash;
                        var updateResult = await _userManager.UpdateAsync(user);



                        if (updateResult.Succeeded)
                        {
                            // Kullanıcıyı oturumdan çıkar
                            await _signInManager.SignOutAsync();

                            // Kullanıcıyı giriş sayfasına yönlendir
                            return RedirectToAction("Login", "Home");
                        }
                        else
                        {
                            foreach (var error in updateResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }

                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                else
                {
                    TempData["ErrorMessage"] = "Mail Adresiniz Sistemde Kayıtlı Değil! Tekrar Deneyiniz.";
                    return View(model);
                }

            }

            return View(model);

        }



        public IActionResult Basket()

        {

            var userId = int.Parse(_userManager.GetUserId(User)); // String'i int'e dönüştür
            var cart = _cartService.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = _cartService.GetCartByUserId(userId);
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
            return View();
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




