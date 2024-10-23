using Microsoft.AspNetCore.Mvc;
using conanPlusWebApiApp.Models;
using conanPlusWebApiApp.Dal;
using Microsoft.AspNetCore.Authorization;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        private readonly ICommonRepository<AboutUs> _repository;

        public AboutUsController(ICommonRepository<AboutUs> repository)
        {
            _repository = repository;
        }

        // Get About Us (Accessible to everyone)
        [HttpGet]
        [AllowAnonymous] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var aboutUsList = await _repository.GetAll();
                if (aboutUsList == null || aboutUsList.Count == 0)
                {
                    return NotFound("No About Us information found.");
                }

                var aboutUs = aboutUsList.FirstOrDefault(); // Get the first record
                return Ok(aboutUs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update About Us (Only for Admin)
        [HttpPut]
        [Authorize(Roles = "Admin")]  
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(AboutUs aboutUs)
        {
            if (aboutUs == null || string.IsNullOrWhiteSpace(aboutUs.Title) || string.IsNullOrWhiteSpace(aboutUs.Description))
            {
                return BadRequest("Invalid data. Title and Description are required.");
            }

            try
            {
                var aboutUsList = await _repository.GetAll();
                var existingAboutUs = aboutUsList.FirstOrDefault();
                if (existingAboutUs == null)
                {
                    return NotFound("No About Us information found.");
                }

                // Detach the existing entity to avoid tracking conflicts
                _repository.Detach(existingAboutUs);

                // Ensure the correct ID is assigned to the incoming data
                aboutUs.AboutUsId = existingAboutUs.AboutUsId;

                await _repository.Update(aboutUs);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the About Us information: {ex.Message}");
            }
        }
    }
}
