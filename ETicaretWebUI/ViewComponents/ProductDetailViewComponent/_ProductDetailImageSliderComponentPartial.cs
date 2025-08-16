using ETicaret.DtoLayer.ProductDto;
using ETicaretWebUI.Dtos.ProductDtos;
using ETicaretWebUI.Dtos.ProductImageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ETicaretWebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailImageSliderComponentPartial:ViewComponent
    {
        //ürünleri listeleyeceğimiz backend kodu
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7068/api/ProductImages/ProductImagesByProductId?id="+id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductImageGetDto>(jsonData);
                return View(product);
            }
            return View();
        }
        
       
    }
}
