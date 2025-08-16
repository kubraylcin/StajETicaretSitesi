using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.ProductImageDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        private readonly IMapper _mapper;

        public ProductImagesController(IProductImageService productImageService, IMapper mapper)
        {
            _productImageService = productImageService;
            _mapper = mapper;
        }

        // GET: api/ProductImages
        [HttpGet]
        public IActionResult ProductImageList()
        {
            var images = _productImageService.TGetListAll();
            var dtoList = _mapper.Map<List<ProductImageResultDto>>(images);
            return Ok(dtoList);
        }

        [HttpGet("ProductImagesByProductId")]
        public async Task<IActionResult> ProductImagesByProductId(int id)
        {
            var images = await _productImageService.GetByProductIdProductImageAsync(id);
            return Ok(images);
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public IActionResult GetProductImageById(int id)
        {
            var image = _productImageService.TGetById(id);
            if (image == null)
                return NotFound();

            var dto = _mapper.Map<ProductImageResultDto>(image);
            return Ok(dto);
        }

        // POST: api/ProductImages
        [HttpPost]
        public IActionResult CreateProductImage(ProductImageCreateDto createDto)
        {
            var entity = _mapper.Map<ProductImage>(createDto);
            _productImageService.TAdd(entity);
            return Ok("Resim başarıyla eklendi.");
        }

        // PUT: api/ProductImages
        [HttpPut]
        public IActionResult UpdateProductImage(ProductImageUpdateDto updateDto)
        {
            var entity = _mapper.Map<ProductImage>(updateDto);
            _productImageService.TUpdate(entity);
            return Ok("Resim başarıyla güncellendi.");
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProductImage(int id)
        {
            var image = _productImageService.TGetById(id);
            if (image == null)
                return NotFound();

            _productImageService.TDelete(image);
            return Ok("Resim başarıyla silindi.");
        }
    }
}
 

