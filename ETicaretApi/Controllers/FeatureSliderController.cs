using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.FeatureSliderDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FeatureSliderController : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService;
        private readonly IMapper _mapper;

        public FeatureSliderController(IFeatureSliderService featureSliderService, IMapper mapper)
        {
            _featureSliderService = featureSliderService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllFeatureSliders()
        {
            var list = _featureSliderService.TGetListAll();
            var dtoList = _mapper.Map<List<ResultFeatureSliderDto>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetFeatureSliderById(int id)
        {
            var entity = _featureSliderService.TGetById(id);
            if (entity == null) return NotFound();
            var dto = _mapper.Map<ResultFeatureSliderDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateFeatureSlider(CreateFeatureSliderDto createDto)
        {
            var entity = _mapper.Map<FeatureSlider>(createDto);
            _featureSliderService.TAdd(entity);
            return Ok("Ekleme işlemi tamamlandı.");
        }

        [HttpPut]
        public IActionResult UpdateFeatureSlider(UpdateFeatureSliderDto updateDto)
        {
            var entity = _mapper.Map<FeatureSlider>(updateDto);
            _featureSliderService.TUpdate(entity);
            return Ok("Güncelleme işlemi tamamlandı.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeatureSlider(int id)
        {
            var entity = _featureSliderService.TGetById(id);
            if (entity == null) return NotFound();
            _featureSliderService.TDelete(entity);
            return Ok("Silme işlemi tamamlandı.");
        }

        [HttpGet("ChangeStatusToTrue/{id}")]
        public async Task<IActionResult> ChangeStatusToTrue(string id)
        {
            await _featureSliderService.TFeatureSliderChangeStatusToTrue(id);
            return Ok("Status true yapıldı.");
        }

        [HttpGet("ChangeStatusToFalse/{id}")]
        public async Task<IActionResult> ChangeStatusToFalse(string id)
        {
            await _featureSliderService.TFeatureSliderChangeStatusToFalse(id);
            return Ok("Status false yapıldı.");
        }
    }
}

