using Microsoft.AspNetCore.Mvc;
using conanPlusWebApiApp.Models;
using conanPlusWebApiApp.Dal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomServiceBtnLinkController : ControllerBase
    {
        private readonly ICommonRepository<CustomServiceBtnLink> _repository;

        public CustomServiceBtnLinkController(ICommonRepository<CustomServiceBtnLink> repository)
        {
            _repository = repository;
        }

        // Get the current custom service link
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var customServiceLinks = await _repository.GetAll();
            if (customServiceLinks == null || customServiceLinks.Count == 0)
            {
                return NotFound("No custom service link found.");
            }
            var customServiceLink = customServiceLinks.FirstOrDefault(); // Get the first link
            return Ok(customServiceLink);
        }

        // Update the custom service link (replace the existing one)
        [HttpPut]
        [Authorize(Policy = "AdminPolicy")] // Access only for Admins
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(CustomServiceBtnLink link)
        {
            if (string.IsNullOrWhiteSpace(link.Link))
            {
                return BadRequest("Link cannot be null or empty.");
            }

            if (!Uri.IsWellFormedUriString(link.Link, UriKind.Absolute))
            {
                return BadRequest("The provided link is not a valid URL.");
            }

            var customServiceLinks = await _repository.GetAll();
            var existingLink = customServiceLinks.FirstOrDefault();

            if (existingLink == null)
            {
                return NotFound("No custom service link found.");
            }

            _repository.Detach(existingLink);
            link.CustomServiceId = existingLink.CustomServiceId;

            try
            {
                await _repository.Update(link);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }
    }
}
