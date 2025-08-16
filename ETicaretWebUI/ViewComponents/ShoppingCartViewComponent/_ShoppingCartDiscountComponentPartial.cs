using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebUI.ViewComponents.ShoppingCartViewComponent
{
    public class _ShoppingCartDiscountComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
