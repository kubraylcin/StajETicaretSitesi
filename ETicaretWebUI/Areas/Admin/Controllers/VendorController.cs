using ETicaretWebUI.Dtos.VendorDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ETicaretWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Vendor")]
    [Authorize]
    public class VendorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VendorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v0 = "Vendor İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Vendorlar";
            ViewBag.v3 = "Vendor Listesi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7068/api/Vendors");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<VendorResultDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("CreateVendor")]
        [HttpGet]
        public IActionResult CreateVendor()
        {
            ViewBag.v0 = "Vendor İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Vendorlar";
            ViewBag.v3 = "Yeni Vendor Girişi";
            return View();
        }

        [Route("CreateVendor")]
        [HttpPost]
        public async Task<IActionResult> CreateVendor(VendorCreateDto createVendorDto)
        {
            if (createVendorDto.ImageFile == null || createVendorDto.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Bir görsel dosyası seçmelisiniz.");
                return View();
            }

            using var content = new MultipartFormDataContent();

            // Dosya içeriğini ekle
            var fileStream = createVendorDto.ImageFile.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(createVendorDto.ImageFile.ContentType);
            content.Add(fileContent, "ImageFile", createVendorDto.ImageFile.FileName);

            // Diğer alanları ekle
            content.Add(new StringContent(createVendorDto.VendorName), "VendorName");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsync("https://localhost:7068/api/Vendors", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Vendor", new { area = "Admin" });
            }

            return View();
        }

        [Route("DeleteVendor/{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7068/api/Vendors/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Vendor", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateVendor/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateVendor(int id)
        {
            ViewBag.v0 = "Vendor İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Vendorlar";
            ViewBag.v3 = "Vendor Güncelleme Sayfası";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7068/api/Vendors/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var vendor = JsonConvert.DeserializeObject<VendorUpdateDto>(jsonData);
                return View(vendor);
            }

            return NotFound();
        }

        [Route("UpdateVendor/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateVendor(VendorUpdateDto updateVendorDto)
        {
            using var content = new MultipartFormDataContent();

            if (updateVendorDto.ImageFile != null && updateVendorDto.ImageFile.Length > 0)
            {
                var fileStream = updateVendorDto.ImageFile.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(updateVendorDto.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", updateVendorDto.ImageFile.FileName);
            }

            content.Add(new StringContent(updateVendorDto.VendorId.ToString()), "VendorId");
            content.Add(new StringContent(updateVendorDto.VendorName), "VendorName");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsync($"https://localhost:7068/api/Vendors/{updateVendorDto.VendorId}", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Vendor", new { area = "Admin" });
            }

            return View(updateVendorDto);
        }
    }
}
