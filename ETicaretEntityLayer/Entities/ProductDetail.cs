using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETicaretEntityLayer.Entities
{
    public class ProductDetail
    {
        public int ProductDetailID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductInfo { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
