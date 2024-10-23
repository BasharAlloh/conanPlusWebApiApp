using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly ICommonRepository<ContactInfo> _contactInfoRepository;

        public ContactInfoController(ICommonRepository<ContactInfo> contactInfoRepository)
        {
            _contactInfoRepository = contactInfoRepository;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactInfo>> GetContactInfo(int id)
        {
            try
            {
                var contactInfo = await _contactInfoRepository.GetDetails(id);
                if (contactInfo == null)
                {
                    return NotFound("Contact information not found.");
                }
                return Ok(contactInfo);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Get the contact info (assuming there's only one record)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactInfo>> GetContactInfo()
        {
            try
            {
                var contactInfo = await _contactInfoRepository.GetAll();

                if (contactInfo == null || contactInfo.Count == 0)
                {
                    return NotFound("Contact information not found.");
                }

                // Assuming there's only one record, return the first
                return Ok(contactInfo[0]);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContactInfoPartial(int id, [FromBody] ContactInfo updatedContactInfo)
        {
            if (updatedContactInfo == null)
            {
                return BadRequest("Invalid contact info.");
            }

            try
            {
                var contactInfo = await _contactInfoRepository.GetDetails(id);
                if (contactInfo == null)
                {
                    return NotFound("Contact info not found.");
                }

                // Fill missing fields with existing values from the database
                updatedContactInfo.ContactId = contactInfo.ContactId; // Always keep the original ID
                updatedContactInfo.Phone = !string.IsNullOrEmpty(updatedContactInfo.Phone) ? updatedContactInfo.Phone : contactInfo.Phone;
                updatedContactInfo.Address = !string.IsNullOrEmpty(updatedContactInfo.Address) ? updatedContactInfo.Address : contactInfo.Address;
                updatedContactInfo.WorkingHours = !string.IsNullOrEmpty(updatedContactInfo.WorkingHours) ? updatedContactInfo.WorkingHours : contactInfo.WorkingHours;
                updatedContactInfo.Email = !string.IsNullOrEmpty(updatedContactInfo.Email) ? updatedContactInfo.Email : contactInfo.Email;
                updatedContactInfo.WhatsApp = !string.IsNullOrEmpty(updatedContactInfo.WhatsApp) ? updatedContactInfo.WhatsApp : contactInfo.WhatsApp;
                updatedContactInfo.Instagram = !string.IsNullOrEmpty(updatedContactInfo.Instagram) ? updatedContactInfo.Instagram : contactInfo.Instagram;

                // Save the updated contact info
                await _contactInfoRepository.Update(updatedContactInfo);

                return Ok(updatedContactInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
