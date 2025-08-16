using AutoMapper;
using ETicaret.BusinessLayer.Abstract;

using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETicaret.DtoLayer.VendorDto;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;

        public VendorsController(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult VendorList()
        {
            var vendorList = _vendorService.TGetListAll();
            var dtoList = _mapper.Map<List<VendorResultDto>>(vendorList);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public IActionResult GetVendorById(int id)
        {
            var vendor = _vendorService.TGetById(id);
            if (vendor == null) return NotFound();
            var dto = _mapper.Map<VendorResultDto>(vendor);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromForm] VendorCreateDto createDto)
        {
            string? imagePath = null;

            if (createDto.ImageFile != null && createDto.ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(createDto.ImageFile.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ETicaretWebUI", "wwwroot", "imageees", "vendors");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await createDto.ImageFile.CopyToAsync(stream);

                imagePath = "/imageees/vendors/" + fileName;
            }

            var entity = _mapper.Map<Vendor>(createDto);
            entity.ImagePath = imagePath;

            _vendorService.TAdd(entity);

            return Ok("Marka başarıyla oluşturuldu.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVendor(int id)
        {
            var vendor = _vendorService.TGetById(id);
            if (vendor == null) return NotFound();
            _vendorService.TDelete(vendor);
            return Ok("Maeka başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVendor([FromForm] VendorUpdateDto updateDto)
        {
            string? imagePath = null;

            if (updateDto.ImageFile != null && updateDto.ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(updateDto.ImageFile.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ETicaretWebUI", "wwwroot", "images", "vendors");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await updateDto.ImageFile.CopyToAsync(stream);

                imagePath = "/images/vendors/" + fileName;
            }

            var entity = _mapper.Map<Vendor>(updateDto);

            if (imagePath != null)
                entity.ImagePath = imagePath;

            _vendorService.TUpdate(entity);

            return Ok("Vendor başarıyla güncellendi.");
        }
    }
}
