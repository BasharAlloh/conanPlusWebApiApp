using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisionController : ControllerBase
    {
        private readonly ICommonRepository<Vision> _visionRepository;

        public VisionController(ICommonRepository<Vision> visionRepository)
        {
            _visionRepository = visionRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVisions()
        {
            try
            {
                var visions = await _visionRepository.GetAll();
                return Ok(visions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVisionById(int id)
        {
            try
            {
                var vision = await _visionRepository.GetDetails(id);
                if (vision == null)
                {
                    return NotFound("Vision not found");
                }

                return Ok(vision);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVision(int id, Vision vision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vision.VisionId)
            {
                return BadRequest("Vision ID mismatch");
            }

            try
            {
                var existingVision = await _visionRepository.GetDetails(id);
                if (existingVision == null)
                {
                    return NotFound("Vision not found");
                }

                var updatedVision = await _visionRepository.Update(vision);
                return Ok(updatedVision);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
    }
}
