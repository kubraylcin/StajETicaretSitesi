using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DtoLayer.ProductImageDto
{
    public class ProductImageGetDto
    {
        public int ProductImageID { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public int ProductID { get; set; }
    }
}
