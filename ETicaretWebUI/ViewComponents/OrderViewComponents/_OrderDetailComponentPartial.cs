using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebUI.ViewComponents.OrderViewComponents
{
    public class _OrderDetailComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
