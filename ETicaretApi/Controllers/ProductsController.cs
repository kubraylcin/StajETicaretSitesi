using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.ProductDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
       
        [HttpGet]
        public IActionResult ProductList()
        {
            var products = _productService.TGetListAll();
            var result = _mapper.Map<List<ProductResultDto>>(products);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.TGetById(id);
            if (product == null)
                return NotFound();

            var dto = _mapper.Map<ProductResultDto>(product);
            return Ok(dto);
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductCreateDto createDto)
        {
            var product = _mapper.Map<Product>(createDto);
            _productService.TAdd(product);
            return Ok("Ürün başarıyla eklendi.");
        }   
        [HttpPut]
        public IActionResult UpdateProduct(ProductUpdateDto updateDto)
        {
            var product = _mapper.Map<Product>(updateDto);
            _productService.TUpdate(product);
            return Ok("Ürün başarıyla güncellendi.");
        }
       
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.TGetById(id);
            if (product == null)
                return NotFound();

            _productService.TDelete(product);
            return Ok("Ürün başarıyla silindi.");
        }
        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _productService.GetProductsWithCategoryAsync();
            return Ok(values);
        }
        [HttpGet("ProductsWithCategoryByCategoryId")]
        public async Task<IActionResult> ProductsWithCategoryByCategoryId(int id, int page = 1, int pageSize = 10)
        {
            var allProducts = await _productService.GetProductsWithCategoryByCategoryIdAsync(id);

            // Sayfalama yapıyoruz
            var pagedProducts = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(pagedProducts);
        }

        [HttpGet("AllProducts")]
        public async Task<IActionResult> AllProducts(int page = 1, int pageSize = 10)
        {
            var allProducts = _productService.TGetListAll();
            var pagedProducts = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(pagedProducts);
        }
    }
}
 
