using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using conanPlusWebApiApp.Models;
using conanPlusWebApiApp.Dal;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly ICommonRepository<Partner> _partnerRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PartnerController(ICommonRepository<Partner> partnerRepository, IWebHostEnvironment hostEnvironment)
        {
            _partnerRepository = partnerRepository;
            _hostEnvironment = hostEnvironment;
        }

        // Add new partner
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePartner([FromForm] IFormFile imageFile, [FromForm] string partnerName)
        {
            if (imageFile == null || string.IsNullOrEmpty(partnerName))
            {
                return BadRequest("Partner name and image file are required.");
            }

            try
            {
                // إنشاء مسار المجلد لحفظ الصورة
                var folderPath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", partnerName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var newPartner = new Partner
                {
                    PartnerName = partnerName,
                    // تحويل الفواصل العكسية إلى فواصل مائلة
                    ImageFileName = Path.Combine("uploads", partnerName, fileName).Replace("\\", "/")
                };

                await _partnerRepository.Insert(newPartner);
                return CreatedAtAction(nameof(GetPartnerById), new { id = newPartner.PartnerId }, newPartner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update existing partner
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePartner(int id, [FromForm] IFormFile imageFile, [FromForm] string partnerName)
        {
            if (string.IsNullOrEmpty(partnerName))
            {
                return BadRequest("Partner name is required.");
            }

            try
            {
                var existingPartner = await _partnerRepository.GetDetails(id);
                if (existingPartner == null)
                {
                    return NotFound("Partner not found.");
                }

                existingPartner.PartnerName = partnerName;

                if (imageFile != null)
                {
                    var folderPath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", partnerName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, existingPartner.ImageFileName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    var newFileName = Path.GetFileName(imageFile.FileName);
                    var newFilePath = Path.Combine(folderPath, newFileName);

                    using (var stream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // تحويل الفواصل العكسية إلى فواصل مائلة
                    existingPartner.ImageFileName = Path.Combine("uploads", partnerName, newFileName).Replace("\\", "/");
                }

                await _partnerRepository.Update(existingPartner);
                return Ok(existingPartner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get partner by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPartnerById(int id)
        {
            try
            {
                var partner = await _partnerRepository.GetDetails(id);
                if (partner == null)
                {
                    return NotFound("Partner not found.");
                }
                return Ok(partner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get all partners
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPartners()
        {
            try
            {
                var partners = await _partnerRepository.GetAll();
                if (partners == null || !partners.Any())
                {
                    return NotFound("No Partners Found");
                }
                return Ok(partners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete partner
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePartner(int id)
        {
            try
            {
                var partner = await _partnerRepository.GetDetails(id);
                if (partner == null)
                {
                    return NotFound("Partner not found.");
                }

                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, partner.ImageFileName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                await _partnerRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
