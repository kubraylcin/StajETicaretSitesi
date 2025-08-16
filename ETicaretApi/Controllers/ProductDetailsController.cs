using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.ProductDetailDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;
        private readonly IMapper _mapper;

        public ProductDetailsController(IProductDetailService productDetailService, IMapper mapper)
        {
            _productDetailService = productDetailService;
            _mapper = mapper;
        }

        // GET: api/ProductDetails
        [HttpGet]
        public IActionResult ProductDetailList()
        {
            var productDetails = _productDetailService.TGetListAll();
            var result = _mapper.Map<List<ProductDetailResultDto>>(productDetails);
            return Ok(result);
        }

        // GET: api/ProductDetails/5
        [HttpGet("{id}")]
        public IActionResult GetProductDetailById(int id)
        {
            var detail = _productDetailService.TGetById(id);
            return Ok(detail);
        }
        [HttpGet("GetProductDetailByIdByProductId")]
        public async Task<IActionResult> GetProductDetailByIdByProductId(int id)
        {
            var productDetailDto = await _productDetailService.GetByProductIdProductDetailAsync(id);
             if (productDetailDto == null)
            {
                // Return 404 Not Found if no product detail is found for the given ProductID
                return NotFound($"Product detail for ProductID {id} not found.");
            }

            // Return 200 OK with the ProductDetailGetDto
            return Ok(productDetailDto);
        }
        

        // POST: api/ProductDetails
        [HttpPost]
        public IActionResult CreateProductDetail(ProductDetailCreateDto createDto)
        {
            var entity = _mapper.Map<ProductDetail>(createDto);
            _productDetailService.TAdd(entity);
            return Ok("Ekleme işlemi başarılı.");
        }

        // PUT: api/ProductDetails
        [HttpPut]
        public IActionResult UpdateProductDetail(ProductDetailUpdateDto updateDto)
        {
            var entity = _mapper.Map<ProductDetail>(updateDto);
            _productDetailService.TUpdate(entity);
            return Ok("Güncelleme işlemi başarılı.");
        }

        // DELETE: api/ProductDetails/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProductDetail(int id)
        {
            var detail = _productDetailService.TGetById(id);
            if (detail == null)
                return NotFound();

            _productDetailService.TDelete(detail);
            return Ok("Silme işlemi başarılı.");
        }
    }
}
 