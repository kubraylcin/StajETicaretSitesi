using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.AddressDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ETicaretApi.Controllers
{
    //[Authorize]  // Sadece giriş yapmış kullanıcılar erişebilir
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddressList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            // Kullanıcının sadece kendi adreslerini listele
            var addresses = _addressService.TGetListAll()
                                .Where(a => a.UserID == userId)
                                .ToList();

            var dtoList = _mapper.Map<List<AddressGetDto>>(addresses);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            var address = _addressService.TGetById(id);
            if (address == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (address.UserID != userId)
                return Forbid("Bu adrese erişim yetkiniz yok.");

            var dto = _mapper.Map<AddressGetDto>(address);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressCreateDto createDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            // Kullanıcının ID'sini DTO'ya ekle
            createDto.UserID = userId;

            var entity = _mapper.Map<Address>(createDto);
            _addressService.TAdd(entity);
            return Ok("Adres başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _addressService.TGetById(id);
            if (address == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (address.UserID != userId)
                return Forbid("Bu adresi silme yetkiniz yok.");

            _addressService.TDelete(address);
            return Ok("Adres başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateAddress([FromBody] AddressUpdateDto updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            var address = _addressService.TGetById(updateDto.AddressID);
            if (address == null) return NotFound();

            if (address.UserID != userId)
                return Forbid("Bu adresi güncelleme yetkiniz yok.");

            // Güncelleme öncesi UserID kesinlikle aynı kalmalı
            updateDto.UserID = userId;

            var entity = _mapper.Map<Address>(updateDto);
            _addressService.TUpdate(entity);
            return Ok("Adres başarıyla güncellendi.");
        }
    }
}
