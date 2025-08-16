using ETicaret.BusinessLayer.Abstract;
using ETicaret.BusinessLayer.Concrete;
using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DataAccessLayer.Concrete;
using ETicaret.DataAccessLayer.Context;
using ETicaret.DtoLayer.Settings;
using ETicaretApi.Mapping;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// JWT Ayarlarını yapılandırma
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Kimlik Doğrulama (Authentication) - JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };
});

// API Controller'ları ekle
builder.Services.AddControllers();

// Swagger ve diğer servisler
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext - SQL Server bağlantısı
builder.Services.AddDbContext<ETicaretContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity kurulumu
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<ETicaretContext>()
    .AddDefaultTokenProviders();

// Redis yapılandırması
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();
    return redis;
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(GeneralMapping).Assembly);

// HttpContext erişimi
builder.Services.AddHttpContextAccessor(); // Bu servis, ILoginService için gereklidir.

// Servis kayıtları
// Business Layer Servisleri
builder.Services.AddScoped<ILoginService, LoginService>(); // Mevcut ILoginService implementasyonunuz
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductDetailService, ProductDetailManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICargoCompanyService, CargoCompanyManager>();
builder.Services.AddScoped<ICargoCustomerService, CargoCustomerManager>();
builder.Services.AddScoped<ICargoDetailService, CargoDetailManager>();
builder.Services.AddScoped<ICargoOperationService, CargoOperationManager>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferManager>();
builder.Services.AddScoped<IFeatureService, FeatureManager>();
builder.Services.AddScoped<IVendorService, VendorManager>();
builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IProductImageService, ProductImageManager>();
builder.Services.AddScoped<IUserCommentService, UserCommentManager>();
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IOrderingService, OrderingManager>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailManager>();
builder.Services.AddScoped<IAddressService, AddressManager>();
// Data Access Layer Servisleri
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<IProductDetailDal, EfProductDetailDal>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICargoCompanyDal, EfCargoCompanyDal>();
builder.Services.AddScoped<ICargoCustomerDal, EfCargoCustomerDal>();
builder.Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>();
builder.Services.AddScoped<ICargoOperationDal, EfCargoOperationDal>();
builder.Services.AddScoped<IFeatureSliderDal, EfFeatureSliderDal>();
builder.Services.AddScoped<ISpecialOfferDal, EfSpecialOfferDal>();
builder.Services.AddScoped<IFeatureDal, EfFeatureDal>();
builder.Services.AddScoped<IVendorDal, EfVendorDal>();
builder.Services.AddScoped<IAboutDal, EfAboutManager>();
builder.Services.AddScoped<IProductImageDal, EfProductImageDal>();
builder.Services.AddScoped<IUserCommentDal, EfUserCommentDal>();
builder.Services.AddScoped<IContactDal, EfContactManager>();
builder.Services.AddScoped<IOrderingDal, EfOrderingDal>();
builder.Services.AddScoped<IOrderDetailDal, EfOrderDetailDal>();
builder.Services.AddScoped<IAddressDal, EfAddressDal>();


var app = builder.Build();

// Uygulama ortamı kontrolü (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware sırası: Routing -> HTTPS Redirection -> Authentication -> Authorization -> Controllers
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication(); // JWT doğrulamasını burada yapar
app.UseAuthorization();  // Kullanıcı yetkilerini kontrol eder

app.MapControllers();

app.Run();