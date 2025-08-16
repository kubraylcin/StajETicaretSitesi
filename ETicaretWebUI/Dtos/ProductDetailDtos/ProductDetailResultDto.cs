using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretWebUI.Dtos.ProductDetailDtos
{
    public class ProductDetailResultDto
    {
        public int ProductDetailID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductInfo { get; set; }
        public int ProductID { get; set; }
    }
}
