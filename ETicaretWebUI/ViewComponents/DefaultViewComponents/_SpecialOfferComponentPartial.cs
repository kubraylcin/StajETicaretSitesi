using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebUI.ViewComponents.DefaultViewComponents
{
    public class _SpecialOfferComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
