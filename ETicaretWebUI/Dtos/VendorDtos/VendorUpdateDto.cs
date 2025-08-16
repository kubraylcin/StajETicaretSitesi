using Microsoft.AspNetCore.Http;

namespace ETicaretWebUI.Dtos.VendorDtos
{
    public class VendorUpdateDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
