using ETicaretWebUI.Dtos.ContactDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ETicaretWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7068/api/Contact");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultContactDto>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            return await UpdateReadStatus(id, true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            return await UpdateReadStatus(id, false);
        }

        private async Task<IActionResult> UpdateReadStatus(int id, bool isRead)
        {
            var client = _httpClientFactory.CreateClient();
            var getResponse = await client.GetAsync($"https://localhost:7068/api/Contact/{id}");
            if (!getResponse.IsSuccessStatusCode)
                return NotFound();

            var jsonData = await getResponse.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<ResultContactDto>(jsonData);

            if (contact == null)
                return NotFound();

            contact.IsRead = isRead;

            var updateDto = new
            {
                ContactId = contact.ContactId,
                NameSurname = contact.NameSurname,
                Email = contact.Email,
                Subject = contact.Subject,
                Message = contact.Message,
                IsRead = contact.IsRead,
                SendDate = contact.SendDate
            };

            var content = new StringContent(JsonConvert.SerializeObject(updateDto), Encoding.UTF8, "application/json");
            var putResponse = await client.PutAsync("https://localhost:7068/api/Contact", content);

            if (!putResponse.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Durum güncellenemedi.";
                return StatusCode(500);
            }

            return Ok();
        }


    }
}
