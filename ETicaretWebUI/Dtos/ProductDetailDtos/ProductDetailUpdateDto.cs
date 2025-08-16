using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretWebUI.Dtos.ProductDetailDtos
{
    public class ProductDetailUpdateDto
    {

        [JsonProperty("productDetailID")]
        public int ProductDetailID { get; set; }

        [JsonProperty("productDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty("productInfo")]
        public string ProductInfo { get; set; }

        [JsonProperty("productID")]
        public int ProductID { get; set; }
    }
}
