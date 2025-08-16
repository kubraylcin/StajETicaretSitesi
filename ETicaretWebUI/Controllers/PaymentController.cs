using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebUI.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "Belediye";
            ViewBag.directory2 = "Ödeme Ekranı";
            ViewBag.directory2 = "Kartla Ödeme";
            return View();
        }
    }
}
