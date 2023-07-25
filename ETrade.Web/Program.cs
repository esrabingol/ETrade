using ETrade.Abstract;
using ETrade.Concrete.EntityFraemwork;
using ETrade.Concrete.Manager;
using ETrade.Context;
using ETrade.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HomeContext>(options =>
{
    options.UseSqlServer("DefaultConnection");
});


builder.Services.AddScoped<IAnnouncementRepository, EfCoreAnnouncementRepository>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>(); // MemoryProduct üzerinden gelen ile ayný altyapýya sahip
builder.Services.AddScoped<IProductService, ProductManager>();

//SeedDatabase.Seed();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
