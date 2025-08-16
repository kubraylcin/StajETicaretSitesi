using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ETicaretEntityLayer.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Otomatik arttırılır
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)] 
        public string CategoryName { get; set; }

        public string? ImagePath { get; set; } // Veritabanında sadece dosya yolu saklanır.

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // Bu dosya doğrudan DB'ye kaydedilmez.
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
