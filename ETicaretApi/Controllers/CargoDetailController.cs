using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.CargoDetailDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CargoDetailController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly IMapper _mapper;

        public CargoDetailController(ICargoDetailService cargoDetailService, IMapper mapper)
        {
            _cargoDetailService = cargoDetailService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCargoDetails()
        {
            var cargoDetailList = _cargoDetailService.TGetListAll();
            var dtoList = _mapper.Map<List<ResultCargoDetailDto>>(cargoDetailList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoDetailById(int id)
        {
            var cargoDetail = _cargoDetailService.TGetById(id);
            if (cargoDetail == null) return NotFound();
            var dto = _mapper.Map<ResultCargoDetailDto>(cargoDetail);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateCargoDetail(CreateCargoDetailDto createDto)
        {
            var entity = _mapper.Map<CargoDetail>(createDto);
            _cargoDetailService.TAdd(entity);
            return Ok("Kargo detayı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCargoDetail(int id)
        {
            var cargoDetail = _cargoDetailService.TGetById(id);
            if (cargoDetail == null) return NotFound();
            _cargoDetailService.TDelete(cargoDetail);
            return Ok("Kargo detayı başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDto updateDto)
        {
            var entity = _mapper.Map<CargoDetail>(updateDto);
            _cargoDetailService.TUpdate(entity);
            return Ok("Kargo detayı başarıyla güncellendi.");
        }
    }
}
