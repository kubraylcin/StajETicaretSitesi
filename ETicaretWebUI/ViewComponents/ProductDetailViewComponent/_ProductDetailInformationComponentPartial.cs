using ETicaretWebUI.Dtos.ProductDetailDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ETicaretWebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailInformationComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailInformationComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7068/api/ProductDetails/GetProductDetailByIdByProductId?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var productInfo = JsonConvert.DeserializeObject<ProductDetailUpdateDto>(jsonData);
                return View(productInfo);
            }
            return View();
        }
      
    }
}
