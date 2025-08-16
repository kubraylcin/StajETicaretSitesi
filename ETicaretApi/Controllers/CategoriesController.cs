using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.CategoryDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [AllowAnonymous]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var categories = _categoryService.TGetListAll(); // Bu metodun içinde Include olmalı

            var dtoList = categories.Select(c => new CategoryResultDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                ImagePath = c.ImagePath,
                ProductCount = c.Products.Count
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryService.TGetById(id);
            if (category == null) return NotFound();
            var dto = _mapper.Map<CategoryResultDto>(category);
            return Ok(dto);
        }

        // CategoriesController.cs içindeki CreateCategory metodu
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto createDto)
        {
            string? imagePath = null;

            if (createDto.ImageFile != null && createDto.ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(createDto.ImageFile.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;
                // Burayı "imaGE" değil "image" olarak değiştir
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ETicaretWebUI", "wwwroot", "image", "categories");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await createDto.ImageFile.CopyToAsync(stream);

                // public URL de lowercase "image"
                imagePath = "/image/categories/" + fileName;
            }

            var entity = _mapper.Map<Category>(createDto);
            entity.ImagePath = imagePath;
            _categoryService.TAdd(entity);

            return Ok("Ekleme işlemi başarılı");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.TGetById(id);
            if (category == null) return NotFound();
            _categoryService.TDelete(category);
            return Ok("Silme İşlemi Tamamlandı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromForm] CategoryUpdateDto updateDto)
        {
            string? imagePath = null;

            if (updateDto.ImageFile != null && updateDto.ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(updateDto.ImageFile.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ETicaretWebUI", "wwwroot", "image", "categories");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await updateDto.ImageFile.CopyToAsync(stream);

                imagePath = "/image/categories/" + fileName;
            }

            var entity = _mapper.Map<Category>(updateDto);

            // ❗ Görsel geldiyse güncelle
            if (imagePath != null)
                entity.ImagePath = imagePath;

            _categoryService.TUpdate(entity);
            return Ok("Güncelleme İşlemi Tamamlandı");
        }
    }
}
