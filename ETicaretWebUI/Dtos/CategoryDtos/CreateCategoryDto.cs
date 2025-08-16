using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretWebUI.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public string? ImagePath { get; set; } // Veritabanında sadece dosya yolu saklanır.

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // Bu dosya doğrudan DB'ye kaydedilmez.

    }
}
