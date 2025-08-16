using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretWebUI.Dtos.VendorDtos
{
    public class VendorResultDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
