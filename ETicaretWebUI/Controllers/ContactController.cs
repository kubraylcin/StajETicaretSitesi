using ETicaretWebUI.Dtos.ContactDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ETicaretWebUI.Controllers
{
    
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDto createContactDto)
        {
            createContactDto.IsRead = false;  // Burada false yapıyoruz
            createContactDto.SendDate = DateTime.Now;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var responseMessage = await client.PostAsync("https://localhost:7068/api/Contact", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi.";
                    // Boş bir model ile view döndür
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", $"API Hatası: {responseMessage.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Sunucu ile bağlantı kurulamadı: " + ex.Message);
            }

            // Hata varsa doldurulmuş modelle tekrar göster
            return View(createContactDto);
        }
    }
}
