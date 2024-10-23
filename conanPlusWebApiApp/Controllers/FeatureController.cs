using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly ICommonRepository<Feature> _FeatureRepository;

        public FeatureController(ICommonRepository<Feature> FeatureRepository)
        {
            _FeatureRepository = FeatureRepository;
        }

        // Get all features
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            try
            {
                var features = await _FeatureRepository.GetAll();
                if (features == null || !features.Any())
                {
                    return NotFound("No features found.");
                }
                return Ok(features);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get a single feature by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Feature>> GetoneFeature(int id)
        {
            try
            {
                var feature = await _FeatureRepository.GetDetails(id);
                if (feature == null)
                {
                    return NotFound("Feature not found.");
                }
                return Ok(feature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Create a new feature (Admin only)
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Feature>> Create(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newfeature = await _FeatureRepository.Insert(feature);
                if (newfeature == null)
                {
                    return BadRequest("Failed to create feature.");
                }
                return CreatedAtAction(nameof(GetoneFeature), new { id = newfeature.FeatureId }, newfeature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update an existing feature (Admin only)
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFeature(int id, Feature feature)
        {
            if (id != feature.FeatureId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var existingFeature = await _FeatureRepository.GetDetails(id);
                if (existingFeature == null)
                {
                    return NotFound("Feature not found.");
                }

                _FeatureRepository.Detach(existingFeature);

                await _FeatureRepository.Update(feature);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // Delete a feature (Admin only)
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            try
            {
                var feature = await _FeatureRepository.GetDetails(id);
                if (feature == null)
                {
                    return NotFound("Feature not found.");
                }

                await _FeatureRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
