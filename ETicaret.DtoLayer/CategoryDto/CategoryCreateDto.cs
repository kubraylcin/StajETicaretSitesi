using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DtoLayer.CategoryDto
{
    public class CategoryCreateDto
    {
        public string CategoryName { get; set; }
        public string? ImagePath { get; set; } // Veritabanında sadece dosya yolu saklanır.

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // Bu dosya doğrudan DB'ye kaydedilmez.

    }
}
