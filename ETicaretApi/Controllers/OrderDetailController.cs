using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.OrderDetailDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ETicaretApi.Controllers
{
    [Authorize]  // Giriş yapılmış kullanıcılar erişebilir
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderingService _orderingService;  // Sipariş servisi (kullanıcı kontrolü için)
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IOrderingService orderingService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _orderingService = orderingService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult OrderDetailList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            // Tüm OrderDetail kayıtları
            var allOrderDetails = _orderDetailService.TGetListAll();

            // Kullanıcının siparişlerinin ID'lerini al
            var userOrderingIds = _orderingService.TGetListAll()
                                    .Where(o => o.UserID == userId)
                                    .Select(o => o.OrderingID)
                                    .ToList();

            // Kullanıcının sadece kendi sipariş detaylarını filtrele
            var userOrderDetails = allOrderDetails.Where(od => userOrderingIds.Contains(od.OrderingID)).ToList();

            var dtoList = _mapper.Map<List<OrderDetailGetDto>>(userOrderDetails);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetailById(int id)
        {
            var orderDetail = _orderDetailService.TGetById(id);
            if (orderDetail == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            var ordering = _orderingService.TGetById(orderDetail.OrderingID);
            if (ordering == null || ordering.UserID != userId)
                return Forbid("Bu sipariş detayına erişim yetkiniz yok.");

            var dto = _mapper.Map<OrderDetailGetDto>(orderDetail);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateOrderDetail([FromBody] OrderDetailCreateDto createDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            var ordering = _orderingService.TGetById(createDto.OrderingID);
            if (ordering == null || ordering.UserID != userId)
                return Forbid("Sipariş detayını eklemek için yetkiniz yok.");

            var entity = _mapper.Map<OrderDetail>(createDto);
            _orderDetailService.TAdd(entity);
            return Ok("Sipariş detayı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            var orderDetail = _orderDetailService.TGetById(id);
            if (orderDetail == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            var ordering = _orderingService.TGetById(orderDetail.OrderingID);
            if (ordering == null || ordering.UserID != userId)
                return Forbid("Bu sipariş detayını silme yetkiniz yok.");

            _orderDetailService.TDelete(orderDetail);
            return Ok("Sipariş detayı başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateOrderDetail([FromBody] OrderDetailUpdateDto updateDto)
        {
            var orderDetail = _orderDetailService.TGetById(updateDto.OrderDetailID);
            if (orderDetail == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Kullanıcı bilgisi alınamadı.");

            var ordering = _orderingService.TGetById(orderDetail.OrderingID);
            if (ordering == null || ordering.UserID != userId)
                return Forbid("Bu sipariş detayını güncelleme yetkiniz yok.");

            updateDto.OrderingID = orderDetail.OrderingID; // Güncelleme sırasında sipariş ilişkisi değişmemeli
            var entity = _mapper.Map<OrderDetail>(updateDto);
            _orderDetailService.TUpdate(entity);
            return Ok("Sipariş detayı başarıyla güncellendi.");
        }
    }
}
