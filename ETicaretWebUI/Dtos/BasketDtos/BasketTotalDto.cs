using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretWebUI.Dtos.BasketDtos
{
    public class BasketTotalDto
    {
        public string UserID { get; set; }
       
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice => BasketItems?.Sum(x => x.Price * x.Quantity) ?? 0;
        public decimal ShippingFee => 0;

        // Genel toplam = sadece ürünlerin fiyatı
        public decimal TotalPriceWithShipping => TotalPrice + ShippingFee;
    }
}
