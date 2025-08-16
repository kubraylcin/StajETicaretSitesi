using ETicaretWebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ETicaretWebUI.ViewComponents.ProductListViewComponentPartial
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id = 0, int page = 1)
        {
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage;

            if (id == 0)
            {
                responseMessage = await client.GetAsync($"https://localhost:7068/api/Products/AllProducts?page={page}&pageSize=9");
            }
            else
            {
                responseMessage = await client.GetAsync($"https://localhost:7068/api/Products/ProductsWithCategoryByCategoryId?id={id}&page={page}&pageSize=9");
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
                return View(values);
            }

            return View(new List<ResultProductWithCategoryDto>());
        }
    }
}
