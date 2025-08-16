using ETicaretWebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ETicaretWebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CategoriesDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            
            var responseMessage = await client.GetAsync("https://localhost:7068/api/Categories"); // API portu

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                string uiBaseUrl = "https://localhost:7247"; // UI projenin base URL'si

                foreach (var category in values)
                {
                    if (!string.IsNullOrEmpty(category.ImagePath))
                    {
                        // Eğer sadece dosya adıysa başına klasör yolunu ekle
                        if (!category.ImagePath.StartsWith("/"))
                            category.ImagePath = "/image/categories/" + category.ImagePath;

                        // Eğer URL tam değilse base url ile birleştir
                        if (!category.ImagePath.StartsWith("http"))
                            category.ImagePath = uiBaseUrl + category.ImagePath;
                    }
                }

                return View(values);
            }

            return View(new List<ResultCategoryDto>());
        }
    }
}
