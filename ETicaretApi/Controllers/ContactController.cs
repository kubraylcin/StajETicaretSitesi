using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DtoLayer.ContactDto;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var contactList = _contactService.TGetListAll();
            // Entity listesini ResultContactDto listesine map'liyoruz
            var dtoList = _mapper.Map<List<ResultContactDto>>(contactList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactService.TGetById(id);
            if (contact == null) return NotFound("İletişim mesajı bulunamadı.");
            // Entity'yi ResultContactDto'ya map'liyoruz
            var dto = _mapper.Map<ResultContactDto>(contact);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createDto) // Yeni bir DTO oluşturdum
        {
            // CreateContactDto'yu Contact entity'sine map'liyoruz
            var entity = _mapper.Map<Contact>(createDto);
            _contactService.TAdd(entity);
            return Ok("İletişim mesajı başarıyla gönderildi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _contactService.TGetById(id);
            if (contact == null) return NotFound("Silinecek iletişim mesajı bulunamadı.");
            _contactService.TDelete(contact);
            return Ok("İletişim mesajı başarıyla silindi.");
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateDto) // Yeni bir DTO oluşturdum
        {
            // UpdateContactDto'yu Contact entity'sine map'liyoruz
            var entity = _mapper.Map<Contact>(updateDto);
            _contactService.TUpdate(entity);
            return Ok("İletişim mesajı başarıyla güncellendi.");
        }
    }
}


