using ETicaret.BusinessLayer.Abstract;
using ETicaretWebUI.Dtos.BasketDtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaretWebUI.ViewComponents.ShoppingCartViewComponent
{
    public class _ShoppingCartProductListComponentPartial:ViewComponent
    {
        private readonly IBasketService _basketService;

        public _ShoppingCartProductListComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userID = (User as ClaimsPrincipal)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userID))
                return View("Empty"); // Kullanıcı login değilse veya sepette ürün yoksa

            var basketTotal = await _basketService.GetBasketAsync(userID);
            var basketItems = basketTotal?.BasketItems;

            if (basketItems == null || !basketItems.Any())
                return View("Empty");

            // Backend DTO listesini UI DTO listesine dönüştür
            var uiBasketItems = basketItems.Select(x => new BasketItemDto
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                Price = x.Price,
                Quantity = x.Quantity,
                ProductImageUrl = x.ProductImageUrl
            }).ToList();

            return View(uiBasketItems);
        }
    }
}
