using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.UserCommentDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class UserCommentController : ControllerBase
    {
        private readonly IUserCommentService _userCommentService;
        private readonly IMapper _mapper;

        public UserCommentController(IUserCommentService userCommentService, IMapper mapper)
        {
            _userCommentService = userCommentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var comments = _userCommentService.TGetListAll();
            var dtoList = _mapper.Map<List<UserCommentResultDto>>(comments);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            var comment = _userCommentService.TGetById(id);
            if (comment == null) return NotFound();
            var dto = _mapper.Map<UserCommentResultDto>(comment);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromForm] UserCommentCreateDto createDto)
        {
            string? imagePath = null;

            if (createDto.ImageFile != null && createDto.ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(createDto.ImageFile.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ETicaretWebUI", "wwwroot", "image", "comments");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await createDto.ImageFile.CopyToAsync(stream);

                imagePath = "/image/comments/" + fileName;
            }

            var comment = _mapper.Map<UserComment>(createDto);
            comment.ImagePath = imagePath;
            comment.CreatedDate = DateTime.Now;
            comment.Status = true;

            _userCommentService.TAdd(comment);
            return Ok("Yorum başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromForm] UserCommentUpdateDto updateDto)
        {
            var existing = _userCommentService.TGetById(updateDto.UserCommentId);
            if (existing == null)
                return NotFound();

            string? imagePath = existing.ImagePath;

            if (updateDto.ImageFile != null && updateDto.ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(updateDto.ImageFile.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ETicaretWebUI", "wwwroot", "image", "comments");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await updateDto.ImageFile.CopyToAsync(stream);

                imagePath = "/image/comments/" + fileName;
            }

            _mapper.Map(updateDto, existing); // eski nesne üzerine güncelleme yap
            existing.ImagePath = imagePath;

            _userCommentService.TUpdate(existing);
            return Ok("Yorum başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _userCommentService.TGetById(id);
            if (comment == null)
                return NotFound();

            _userCommentService.TDelete(comment);
            return Ok("Yorum başarıyla silindi.");
        }

        [HttpGet("CommentListByProductId")]
        public IActionResult CommentListByProductId(int id)
        {
            var comments = _userCommentService.TGetListAll()
                      .Where(x => x.ProductID == id && x.Status == true)
                      .ToList();

            // DTO'ya dönüştürme
            var dtoList = _mapper.Map<List<UserCommentResultDto>>(comments);

            return Ok(dtoList);
        }


    }
}
