using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebUI.Controllers
{
    
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "Belediye";
            ViewBag.directory2 = "Ana SAyfa";
            ViewBag.directory3 = "Ürün Listesi ";

            return View();
        }
    }
}
