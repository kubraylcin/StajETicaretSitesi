using ETicaretWebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ETicaretWebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FeatureProductsDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // Token'ı kullanıcı claim'lerinden al
            string token = "";

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var claim = identity.FindFirst("beltoken");
                if (claim != null)
                {
                    token = claim.Value;
                }
            }

            // Eğer token varsa, Authorization header'a ekle
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var responseMessage = await client.GetAsync("https://localhost:7068/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }

            // Eğer hata varsa boş liste dön ki View hata almasın
            return View(new List<ResultProductDto>());
        }
    }
}
