using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.CargoCompany;

using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CargoCompanyController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly IMapper _mapper;

        public CargoCompanyController(ICargoCompanyService cargoCompanyService, IMapper mapper)
        {
            _cargoCompanyService = cargoCompanyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCargoCompanies()
        {
            var cargoCompanyList = _cargoCompanyService.TGetListAll();
            var dtoList = _mapper.Map<List<CargoCompanyResultDto>>(cargoCompanyList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCompanyById(int id)
        {
            var cargoCompany = _cargoCompanyService.TGetById(id);
            if (cargoCompany == null) return NotFound();
            var dto = _mapper.Map<CargoCompanyResultDto>(cargoCompany);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateCargoCompany(CargoCompanyCreateDto createDto)
        {
            var entity = _mapper.Map<CargoCompany>(createDto);
            _cargoCompanyService.TAdd(entity);
            return Ok("Kargo Firması başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCargoCompany(int id)
        {
            var cargoCompany = _cargoCompanyService.TGetById(id);
            if (cargoCompany == null) return NotFound();
            _cargoCompanyService.TDelete(cargoCompany);
            return Ok("Kargo Firması başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCargoCompany(CargoCompanyUpdateDto updateDto)
        {
            var entity = _mapper.Map<CargoCompany>(updateDto);
            _cargoCompanyService.TUpdate(entity);
            return Ok("Kargo Firması başarıyla güncellendi.");
        }
    }
}
