using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.DtoLayer.UserCommentDto
{
    public class UserCommentResultDto
    {
        public int UserCommentId { get; set; }
        public string NameSurname { get; set; }
        public string CommentDetail { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
