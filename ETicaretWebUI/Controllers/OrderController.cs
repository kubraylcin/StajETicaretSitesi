using ETicaret.BusinessLayer.Abstract;
using ETicaretEntityLayer.Entities;
using ETicaretWebUI.Dtos.OrderingDtos.OrderAddressDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaretWebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IAddressService _addressService;

        public OrderController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: /Order/Index
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Belediye";
            ViewBag.directory2 = "Sipariş";
            ViewBag.directory3 = "Sipariş İşlemleri";

            return View();
        }

        // POST: /Order/Index
        [HttpPost]
        public IActionResult Index(CreateOrderAddressDto createOrderAddressDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.directory1 = "Belediye";
                ViewBag.directory2 = "Sipariş";
                ViewBag.directory3 = "Sipariş İşlemleri";

                return View(createOrderAddressDto);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }

            createOrderAddressDto.UserID = userId;

            var addressEntity = new Address
            {
                UserID = createOrderAddressDto.UserID,
                Name = createOrderAddressDto.Name,
                Surname = createOrderAddressDto.Surname,
                Email = createOrderAddressDto.Email,
                Phone = createOrderAddressDto.Phone,
                Country = createOrderAddressDto.Country,
                District = createOrderAddressDto.District,
                City = createOrderAddressDto.City,
                Detail1 = createOrderAddressDto.Detail1,
                Detail2 = createOrderAddressDto.Detail2
            };

            _addressService.TAdd(addressEntity);

            return RedirectToAction("Index", "Payment");
        }

    }
}
