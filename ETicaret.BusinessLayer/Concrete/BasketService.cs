using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.BasketDto;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task AddBasketItem(string userID, BasketItemDto basketItemDto)
        {
            var basket = await GetBasketAsync(userID);

            var existingItem = basket.BasketItems.FirstOrDefault(x => x.ProductID == basketItemDto.ProductID);

            if (existingItem != null)
            {
                existingItem.Quantity += basketItemDto.Quantity;
            }
            else
            {
                basket.BasketItems.Add(basketItemDto);
            }

            await SaveBasketAsync(basket);
        }

        public async Task DeleteBasketAsync(string userID)
        {
            await _redisService.GetDb().KeyDeleteAsync(userID);
        }

        public async Task<BasketTotalDto> GetBasketAsync(string userID)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userID);

            if (string.IsNullOrEmpty(existBasket))
            {
                return new BasketTotalDto
                {
                    UserID = userID,
                    BasketItems = new List<BasketItemDto>()
                };
            }

            var basket = JsonSerializer.Deserialize<BasketTotalDto>(existBasket);

            // Null kontrolü yapıldı
            if (basket.BasketItems == null)
                basket.BasketItems = new List<BasketItemDto>();

            return basket;
        }

        public async Task<bool> RemoveBasketItem(string userID, int productId)
        {
            var basket = await GetBasketAsync(userID);

            var itemToRemove = basket.BasketItems.FirstOrDefault(x => x.ProductID == productId);

            if (itemToRemove == null)
                return false;

            basket.BasketItems.Remove(itemToRemove);

            await SaveBasketAsync(basket);

            return true;
        }

        public async Task SaveBasketAsync(BasketTotalDto basketTotalDto)
        {
            if (string.IsNullOrEmpty(basketTotalDto.UserID))
                throw new ArgumentException("UserID null veya boş olamaz");

            var serializedBasket = JsonSerializer.Serialize(basketTotalDto);
            await _redisService.GetDb().StringSetAsync(basketTotalDto.UserID, serializedBasket);
        }
    }
}
