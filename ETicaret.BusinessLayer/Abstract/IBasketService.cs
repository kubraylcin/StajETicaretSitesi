using ETicaret.DtoLayer.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETicaret.BusinessLayer.Abstract
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasketAsync(string userID);
        Task SaveBasketAsync(BasketTotalDto basketTotalDto);
        Task DeleteBasketAsync(string userID);
        Task AddBasketItem(string userID, BasketItemDto basketItemDto);
        Task <bool> RemoveBasketItem(string userID, int productId);

    }
}
