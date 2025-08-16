using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.CargoOperationDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CargoOperationController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;
        private readonly IMapper _mapper;

        public CargoOperationController(ICargoOperationService cargoOperationService, IMapper mapper)
        {
            _cargoOperationService = cargoOperationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCargoOperations()
        {
            var cargoOperations = _cargoOperationService.TGetListAll();
            var dtoList = _mapper.Map<List<ResultCargoOperationDto>>(cargoOperations);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoOperationById(int id)
        {
            var cargoOperation = _cargoOperationService.TGetById(id);
            if (cargoOperation == null) return NotFound();
            var dto = _mapper.Map<ResultCargoOperationDto>(cargoOperation);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDto createDto)
        {
            var entity = _mapper.Map<CargoOperation>(createDto);
            _cargoOperationService.TAdd(entity);
            return Ok("Kargo operasyonu başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCargoOperation(int id)
        {
            var cargoOperation = _cargoOperationService.TGetById(id);
            if (cargoOperation == null) return NotFound();
            _cargoOperationService.TDelete(cargoOperation);
            return Ok("Kargo operasyonu başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDto updateDto)
        {
            var entity = _mapper.Map<CargoOperation>(updateDto);
            _cargoOperationService.TUpdate(entity);
            return Ok("Kargo operasyonu başarıyla güncellendi.");
        }
    }
}
