using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.FeatureDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;

        public FeatureController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult FeatureList()
        {
            var featureList = _featureService.TGetListAll();
            var dtoList = _mapper.Map<List<ResultFeatureDto>>(featureList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetFeatureById(int id)
        {
            var feature = _featureService.TGetById(id);
            if (feature == null) return NotFound();
            var dto = _mapper.Map<ResultFeatureDto>(feature);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createDto)
        {
            var entity = _mapper.Map<Feature>(createDto);
            _featureService.TAdd(entity);
            return Ok("Ekleme İşlemi Tamamlandı");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeature(int id)
        {
            var feature = _featureService.TGetById(id);
            if (feature == null) return NotFound();
            _featureService.TDelete(feature);
            return Ok("Silme İşlemi Tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto updateDto)
        {
            var entity = _mapper.Map<Feature>(updateDto);
            _featureService.TUpdate(entity);
            return Ok("Güncelleme İşlemi Tamamlandı");
        }
    }
}
    
