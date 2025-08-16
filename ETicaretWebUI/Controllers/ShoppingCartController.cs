using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.BasketDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaretWebUI.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        public ShoppingCartController(IBasketService basketService, IProductService productService)
        {
            _basketService = basketService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.directory1 = "Belediye";
            ViewBag.directory2 = "";
            ViewBag.directory3 = "Ürün Listesi ";

            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userID))
                return RedirectToAction("Index", "Login");

            var values = await _basketService.GetBasketAsync(userID);
            ViewBag.total = values.TotalPrice;
            var totalPriceWithVergi = values.TotalPrice + values.TotalPrice / 100 * 10;
            ViewBag.totalPriceWithVergi = totalPriceWithVergi;
            ViewBag.tax = values.TotalPrice / 100 * 10;

            return View();
        }

        public async Task<IActionResult> AddBasketItem(int id)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userID))
                return RedirectToAction("Index", "Login");

            var values = await _productService.GetByIdProductAsync(id);

            var items = new BasketItemDto
            {
                ProductID = values.ProductId,
                ProductName = values.ProductName,
                Price = values.ProductPrice,
                Quantity = 1,
                ProductImageUrl = values.ProductImageUrl
            };

            await _basketService.AddBasketItem(userID, items);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveBasketItem(int id)
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userID))
                return RedirectToAction("Index", "Login");

            await _basketService.RemoveBasketItem(userID, id);
            return RedirectToAction("Index");
        }
    }
}
