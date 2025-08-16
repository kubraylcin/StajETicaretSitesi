using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebUI.ViewComponents.OrderViewComponents
{
    public class _PaymentMethodComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
