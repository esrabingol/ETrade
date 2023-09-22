using ETrade.Abstract;
using ETrade.Concrete.EntityFraemwork;
using ETrade.Concrete.Manager;
using ETrade.Context;
using ETrade.Services;
using ETrade.Web.Abstract;
using ETrade.Web.Concrete.EntityFraemwork;
using ETrade.Web.Concrete.Manager;
using ETrade.Web.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//HomeContext'te verileri saklamas�n� istiyorum.
builder.Services.AddDbContext<HomeContext>(options =>
{
    options.UseSqlServer("DefaultConnection");
});

builder.Services.AddIdentity<ApplicationUser, Role>()
   .AddEntityFrameworkStores<HomeContext>()
   .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequireNonAlphanumeric = false; // �zel karakter gereksinimi devre d��� b�rak�l�yor
    options.Password.RequireUppercase = false; // B�y�k harf gereksinimi devre d��� b�rak�l�yor
    options.Password.RequireLowercase = false; // K���k harf gereksinimi devre d��� b�rak�l�yor


    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(2);
    options.Lockout.AllowedForNewUsers= true;
    options.User.RequireUniqueEmail= true;
    
});

builder.Services.AddScoped<IAnnouncementRepository, EfCoreAnnouncementRepository>();

builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>(); // MemoryProduct �zerinden gelen ile ayn� altyap�ya sahip

builder.Services.AddScoped<ICartRepository, EfCoreCartRepository>(); 
builder.Services.AddScoped<IFavoriteRepository, EfCoreFavoriteRepository>();

builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddScoped<ICartService,CartManager>();
builder.Services.AddScoped<IFavoriteService,FavoriteManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();//kullan�c� do�rulamak i�in kullan�l�r.

//app.UseAuthorization();//yetki,izin

//app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseRouting();

app.UseAuthorization(); // Yetkilendirme i�lemi routing'den �nce gelmelidir.

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
