using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.OrderingDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ETicaretApi.Controllers
{
    //[Authorize]  // Giriş yapmış kullanıcılar erişebilir
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingController : ControllerBase
    {
        private readonly IOrderingService _orderingService;
        private readonly IMapper _mapper;

        public OrderingController(IOrderingService orderingService, IMapper mapper)
        {
            _orderingService = orderingService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult OrderingList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            // Sadece kendi siparişlerini listele
            var orderingList = _orderingService.TGetListAll()
                                .Where(o => o.UserID == userId)
                                .ToList();

            var dtoList = _mapper.Map<List<OrderingResultDto>>(orderingList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderingById(int id)
        {
            var ordering = _orderingService.TGetById(id);
            if (ordering == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ordering.UserID != userId)
                return Forbid("Bu siparişin detaylarına erişim yetkiniz yok.");

            var dto = _mapper.Map<OrderingResultDto>(ordering);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateOrdering([FromBody] OrderingCreateDto createDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            createDto.UserID = userId;

            var entity = _mapper.Map<Ordering>(createDto);
            _orderingService.TAdd(entity);
            return Ok("Sipariş başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrdering(int id)
        {
            var ordering = _orderingService.TGetById(id);
            if (ordering == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ordering.UserID != userId)
                return Forbid("Bu siparişi silme yetkiniz yok.");

            _orderingService.TDelete(ordering);
            return Ok("Sipariş başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateOrdering([FromBody] OrderingUpdateDto updateDto)
        {
            var ordering = _orderingService.TGetById(updateDto.OrderingID);
            if (ordering == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ordering.UserID != userId)
                return Forbid("Bu siparişi güncelleme yetkiniz yok.");

            // Güncelleme sırasında UserID değişmemeli, aynı kullanıcı olmalı
            updateDto.UserID = userId;

            var entity = _mapper.Map<Ordering>(updateDto);
            _orderingService.TUpdate(entity);
            return Ok("Sipariş başarıyla güncellendi.");
        }
    }
}
