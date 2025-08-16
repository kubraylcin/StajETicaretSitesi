using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.AboutDto;  
using ETicaretEntityLayer.Entities;  
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ETicaretApi.Controllers
{
    //alloanonynmları sildik
   [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;

        public AboutController(IAboutService aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var aboutList = _aboutService.TGetListAll();
            var dtoList = _mapper.Map<List<ResultAboutDto>>(aboutList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetAboutById(int id)
        {
            var about = _aboutService.TGetById(id);
            if (about == null) return NotFound();
            var dto = _mapper.Map<ResultAboutDto>(about);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateAbout([FromBody] CreateAboutDto createDto)
        {
            var entity = _mapper.Map<About>(createDto);
            _aboutService.TAdd(entity);
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAbout(int id)
        {
            var about = _aboutService.TGetById(id);
            if (about == null) return NotFound();
            _aboutService.TDelete(about);
            return Ok("Silme İşlemi Tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateAbout([FromBody] UpdateAboutDto updateDto)
        {
            var entity = _mapper.Map<About>(updateDto);
            _aboutService.TUpdate(entity);
            return Ok("Güncelleme İşlemi Tamamlandı");
        }
    }
}
