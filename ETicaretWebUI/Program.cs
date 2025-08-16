using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ETicaretWebUI.Handlers;
using ETicaret.DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ETicaretEntityLayer.Entities;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.BusinessLayer.Concrete;
using ETicaret.DtoLayer.Settings;
using Microsoft.Extensions.Options;
using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Concrete; // Yeni eklenecek handler'ýn namespace'i

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ETicaretContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, AppRole>()
.AddEntityFrameworkStores<ETicaretContext>()
    .AddDefaultTokenProviders();
// Cookie Authentication yapýlandýrmasý (Web UI oturumu için)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/LogOut";
        options.AccessDeniedPath = "/Pages/AccessDenied";
        options.Cookie.Name = "BelJwt"; // Cookie adý
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.ExpireTimeSpan = TimeSpan.FromDays(1); // Opsiyonel
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor(); // Bu servis, JwtAuthorizationHandler için gereklidir

// JWT Authorization Handler'ý kaydet
builder.Services.AddTransient<JwtAuthorizationHandler>();

// API'ye istek atmak için isimlendirilmiþ HttpClient'ý yapýlandýrma
// Bu client'a JwtAuthorizationHandler'ý ekleyerek her API çaðrýsýna otomatik olarak JWT eklenmesini saðlýyoruz.
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7068/"); // API'nizin temel adresi
}).AddHttpMessageHandler<JwtAuthorizationHandler>(); // Handler'ý buraya ekliyoruz.

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAddressService, AddressManager>();
builder.Services.AddScoped<IAddressDal, EfAddressDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>(); // <-- EKLE
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect(); // Eðer baðlanmasý gerekiyorsa
    return redis;
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication ve Authorization middleware'leri doðru sýrada
app.UseAuthentication(); // Cookie tabanlý kimlik doðrulama
app.UseAuthorization();  // Yetkilendirme

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();