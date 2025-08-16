using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebUI.ViewComponents.UILayoutViewComponents
{
    public class _TopBarUILayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
