using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ICommonRepository<Package> _packageRepository;

        public PackageController(ICommonRepository<Package> packageRepository)
        {
            _packageRepository = packageRepository;
        }

        // Get all packages
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            try
            {
                var packages = await _packageRepository.GetAll();
                if (packages == null || !packages.Any())
                {
                    return NotFound("No packages found.");
                }
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get a single package by id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            try
            {
                var package = await _packageRepository.GetDetails(id);
                if (package == null)
                {
                    return NotFound("Package not found.");
                }
                return Ok(package);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Create a new package (limit to 3 packages)
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Package>> CreatePackage(Package package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Check if the number of packages exceeds the limit
                var currentPackages = await _packageRepository.GetAll();
                if (currentPackages.Count >= 3)
                {
                    return BadRequest("Cannot add more than 3 packages.");
                }

                var newPackage = await _packageRepository.Insert(package);
                return CreatedAtAction(nameof(GetPackage), new { id = newPackage.PackageId }, newPackage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update a package
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePackage(int id, Package package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != package.PackageId)
            {
                return BadRequest("Package ID mismatch.");
            }

            try
            {
                var existingPackage = await _packageRepository.GetDetails(id);
                if (existingPackage == null)
                {
                    return NotFound("Package not found.");
                }

                // Detach the existing tracked entity to avoid tracking conflict
                _packageRepository.Detach(existingPackage);

                await _packageRepository.Update(package);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete a package
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePackage(int id)
        {
            try
            {
                var packageToDelete = await _packageRepository.GetDetails(id);
                if (packageToDelete == null)
                {
                    return NotFound("Package not found.");
                }

                await _packageRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
