using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly ICommonRepository<FAQ> _faqRepository;

        public FAQController(ICommonRepository<FAQ> faqRepository)
        {
            _faqRepository = faqRepository;
        }

        // Get all FAQs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFAQs()
        {
            try
            {
                var faqs = await _faqRepository.GetAll();
                if (faqs == null || faqs.Count == 0)
                {
                    return NotFound("No FAQs found.");
                }
                return Ok(faqs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get FAQ by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFAQById(int id)
        {
            try
            {
                var faq = await _faqRepository.GetDetails(id);
                if (faq == null)
                {
                    return NotFound("FAQ not found.");
                }
                return Ok(faq);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Create a new FAQ
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFAQ(FAQ faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdFaq = await _faqRepository.Insert(faq);
                return CreatedAtAction(nameof(GetFAQById), new { id = createdFaq.FAQId }, createdFaq);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update an existing FAQ
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFAQ(int id, FAQ faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faq.FAQId)
            {
                return BadRequest("FAQ ID mismatch.");
            }

            try
            {
                var existingFaq = await _faqRepository.GetDetails(id);
                if (existingFaq == null)
                {
                    return NotFound("FAQ not found.");
                }

                await _faqRepository.Update(faq);
                return Ok(faq);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete a FAQ
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            try
            {
                var faq = await _faqRepository.GetDetails(id);
                if (faq == null)
                {
                    return NotFound("FAQ not found.");
                }

                await _faqRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
