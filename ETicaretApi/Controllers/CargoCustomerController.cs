using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.CargoCustomerDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CargoCustomerController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;
        private readonly IMapper _mapper;

        public CargoCustomerController(ICargoCustomerService cargoCustomerService, IMapper mapper)
        {
            _cargoCustomerService = cargoCustomerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCargoCustomers()
        {
            var cargoCustomerList = _cargoCustomerService.TGetListAll();
            var dtoList = _mapper.Map<List<ResultCargoCustomer>>(cargoCustomerList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCustomerById(int id)
        {
            var cargoCustomer = _cargoCustomerService.TGetById(id);
            if (cargoCustomer == null) return NotFound();
            var dto = _mapper.Map<ResultCargoCustomer>(cargoCustomer);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateCargoCustomer(CreateCargoCustomerDto createDto)
        {
            var entity = _mapper.Map<CargoCustomer>(createDto);
            _cargoCustomerService.TAdd(entity);
            return Ok("Kargo müşterisi başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCargoCustomer(int id)
        {
            var cargoCustomer = _cargoCustomerService.TGetById(id);
            if (cargoCustomer == null) return NotFound();
            _cargoCustomerService.TDelete(cargoCustomer);
            return Ok("Kargo müşterisi başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDto updateDto)
        {
            var entity = _mapper.Map<CargoCustomer>(updateDto);
            _cargoCustomerService.TUpdate(entity);
            return Ok("Kargo müşterisi başarıyla güncellendi.");
        }
    }
}
