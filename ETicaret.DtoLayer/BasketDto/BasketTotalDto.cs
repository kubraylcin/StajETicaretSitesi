using System.Collections.Generic;
using System.Linq;

namespace ETicaret.DtoLayer.BasketDto
{
    public class BasketTotalDto
    {
        public string UserID { get; set; }

        // Boş liste ile başlatıldı, null referans engellendi
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();

        // Null kontrolü ile toplam fiyat hesaplaması
        public decimal TotalPrice => BasketItems?.Sum(x => x.Price * x.Quantity) ?? 0;
    }
}
