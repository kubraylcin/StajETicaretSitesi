using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.SpecialOfferDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SpecialOfferController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService;
        private readonly IMapper _mapper;

        public SpecialOfferController(ISpecialOfferService specialOfferService, IMapper mapper)
        {
            _specialOfferService = specialOfferService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var offers = _specialOfferService.TGetListAll();
            var dto = _mapper.Map<List<ResultSpecialOfferDto>>(offers);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var offer = _specialOfferService.TGetById(id);
            if (offer == null) return NotFound();
            var dto = _mapper.Map<ResultSpecialOfferDto>(offer);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create(CreateSpecialOfferDto createDto)
        {
            var entity = _mapper.Map<SpecialOffer>(createDto);
            _specialOfferService.TAdd(entity);
            return Ok("Özel TEklif Ekleme işlemi başarılı");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var offer = _specialOfferService.TGetById(id);
            if (offer == null) return NotFound();
            _specialOfferService.TDelete(offer);
            return Ok("Silme işlemi başarılı");
        }

        [HttpPut]
        public IActionResult Update(UpdateSpecialOfferDto updateDto)
        {
            var entity = _mapper.Map<SpecialOffer>(updateDto);
            _specialOfferService.TUpdate(entity);
            return Ok("Özel TEklif Güncelleme işlemi başarılı");
        }
    }
}
  