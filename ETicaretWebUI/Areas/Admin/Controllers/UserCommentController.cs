using ETicaretWebUI.Dtos.UserCommentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ETicaretWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/UserComment")]
    [Authorize]
    public class UserCommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserCommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v0 = "Yorum İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum Listesi";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7068/api/UserComment");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var comments = JsonConvert.DeserializeObject<List<UserCommentResultDto>>(jsonData);
                return View(comments);
            }
            return View();
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.v0 = "Yorum İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yeni Yorum Ekleme";
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(UserCommentCreateDto createDto)
        {
            if (createDto.ImageFile == null || createDto.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Lütfen bir görsel seçiniz.");
                return View();
            }

            using var content = new MultipartFormDataContent();

            var fileStream = createDto.ImageFile.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(createDto.ImageFile.ContentType);
            content.Add(fileContent, "ImageFile", createDto.ImageFile.FileName);

            content.Add(new StringContent(createDto.NameSurname ?? ""), "NameSurname");
            content.Add(new StringContent(createDto.Email ?? ""), "Email");
            content.Add(new StringContent(createDto.CommentDetail ?? ""), "CommentDetail");
            content.Add(new StringContent(createDto.Rating.ToString()), "Rating");
            content.Add(new StringContent(createDto.ProductID.ToString()), "ProductID");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7068/api/UserComment", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "UserComment", new { area = "Admin" });
            }

            return View(createDto);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7068/api/UserComment/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "UserComment", new { area = "Admin" });
            }

            return RedirectToAction("Index", "UserComment", new { area = "Admin" });
        }

        [Route("Update/{id}")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.v0 = "Yorum İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum Güncelleme";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7068/api/UserComment/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var comment = JsonConvert.DeserializeObject<UserCommentUpdateDto>(jsonData);
                return View(comment);
            }

            return NotFound();
        }

        [Route("Update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(UserCommentUpdateDto updateDto)
        {
            using var content = new MultipartFormDataContent();

            if (updateDto.ImageFile != null && updateDto.ImageFile.Length > 0)
            {
                var fileStream = updateDto.ImageFile.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(updateDto.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", updateDto.ImageFile.FileName);
            }

            content.Add(new StringContent(updateDto.UserCommentId.ToString()), "UserCommentId");
            content.Add(new StringContent(updateDto.NameSurname ?? ""), "NameSurname");
            content.Add(new StringContent(updateDto.Email ?? ""), "Email");
            content.Add(new StringContent(updateDto.CommentDetail ?? ""), "CommentDetail");
            content.Add(new StringContent(updateDto.Rating.ToString()), "Rating");
            content.Add(new StringContent(updateDto.Status.ToString()), "Status");
            content.Add(new StringContent(updateDto.ProductID.ToString()), "ProductID");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsync("https://localhost:7068/api/UserComment", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "UserComment", new { area = "Admin" });
            }

            return View(updateDto);
        }
    }
}
