using ETicaret.DtoLayer.UserCommentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ETicaretWebUI.Controllers
{
    
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index(int id = 0, int page = 1)
        {
            ViewBag.i = id;
            ViewBag.Page = page;
            ViewBag.TotalPages = 5; // Backend'den toplam sayfa sayısını alman gerekir (sabit örnek)

            return View();
        }
        public IActionResult ProductDetail(int id)
        {
            ViewBag.x = id;
            return View();
        }
        [HttpGet]
        public async Task<PartialViewResult>AddComment(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7068/api/UserComment/CommentListByProductId?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserCommentResultDto>>(jsonData);

                return PartialView(values);
            }

            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(UserCommentCreateDto userCommentCreateDto)
        {
            var client = _httpClientFactory.CreateClient();

            using var content = new MultipartFormDataContent();

            if (userCommentCreateDto.ImageFile != null && userCommentCreateDto.ImageFile.Length > 0)
            {
                var streamContent = new StreamContent(userCommentCreateDto.ImageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(userCommentCreateDto.ImageFile.ContentType);
                content.Add(streamContent, "ImageFile", userCommentCreateDto.ImageFile.FileName);
            }

            content.Add(new StringContent(userCommentCreateDto.NameSurname ?? ""), "NameSurname");
            content.Add(new StringContent(userCommentCreateDto.Email ?? ""), "Email");
            content.Add(new StringContent(userCommentCreateDto.CommentDetail ?? ""), "CommentDetail");
            content.Add(new StringContent(userCommentCreateDto.Rating.ToString()), "Rating");
            content.Add(new StringContent(userCommentCreateDto.ProductID.ToString()), "ProductID");

            var response = await client.PostAsync("https://localhost:7068/api/UserComment", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductDetail", new { id = userCommentCreateDto.ProductID });
            }

            return View("ProductDetail", userCommentCreateDto.ProductID);
        }
    }
}
