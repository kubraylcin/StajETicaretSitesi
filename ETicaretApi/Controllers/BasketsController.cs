using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.BasketDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ETicaretApi.Controllers
{
    //[Authorize] // Bu kontrolcüye erişim için yetkilendirme gerekli
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILoginService _loginService;

        public BasketsController(IBasketService basketService, ILoginService loginService)
        {
            _basketService = basketService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBasket()
        {
            // Kullanıcı ID'sini _loginService üzerinden alıyoruz.
            // Bu servis, HttpContext.User.Claims içindeki ClaimTypes.NameIdentifier'ı okur.
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest("Kullanıcı oturumu bulunamadı.");

            var basket = await _basketService.GetBasketAsync(userId);
            return Ok(basket);


        }

        [HttpPost("SaveMyBasket")]
        public async Task<IActionResult> SaveMyBasket([FromBody] BasketTotalDto basketTotalDto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            basketTotalDto.UserID = userId;

            await _basketService.SaveBasketAsync(basketTotalDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMyBasket()
        {
            var userId = _loginService.GetUserID;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }

            await _basketService.DeleteBasketAsync(userId);
            return Ok("Sepet Silindi");
        }
    }
}