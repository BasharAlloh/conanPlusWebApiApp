using AutoMapper;
using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly ICommonRepository<Filter> _filterRepository;
        private readonly ICommonRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public FilterController(ICommonRepository<Filter> filterRepository, ICommonRepository<Project> projectRepository, IMapper mapper)
        {
            _filterRepository = filterRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        // Get filters by service id
        [HttpGet("service/{serviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFiltersByServiceId(int serviceId)
        {
            try
            {
                var filters = await _filterRepository.GetAll();
                var serviceFilters = filters.Where(f => f.ServiceId == serviceId).ToList();

                if (serviceFilters == null || serviceFilters.Count == 0)
                {
                    return NotFound("No filters found for the specified service.");
                }

                // إضافة الفلتر "All" يدوياً وإضافة جميع المشاريع المتعلقة بالخدمة المحددة
                var allProjects = await _projectRepository.GetAll();
                var relatedProjects = allProjects.Where(p => p.ServiceId == serviceId).ToList();

                serviceFilters.Insert(0, new Filter
                {
                    FilterId = 0,
                    FilterName = "All",
                    ServiceId = serviceId,
                    Projects = relatedProjects
                });

                var filterDtos = _mapper.Map<IEnumerable<FilterDisplayDTO>>(serviceFilters);
                return Ok(filterDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get a filter by id (returning FilterDisplayDTO)
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFilter(int id)
        {
            try
            {
                var filter = await _filterRepository.GetDetails(id);
                if (filter == null)
                {
                    return NotFound();
                }

                // إذا كان الفلتر هو "All"، استرجاع جميع المشاريع المتعلقة بالخدمة
                if (filter.FilterName.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    var allProjects = await _projectRepository.GetAll();
                    filter.Projects = allProjects.Where(p => p.ServiceId == filter.ServiceId).ToList();
                }

                var filterDisplayDto = _mapper.Map<FilterDisplayDTO>(filter);
                return Ok(filterDisplayDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get all filters (returning List of FilterDisplayDTO)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFilters()
        {
            try
            {
                var filters = await _filterRepository.GetAll();
                var filtersDisplayDto = _mapper.Map<IEnumerable<FilterDisplayDTO>>(filters);
                return Ok(filtersDisplayDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Create a new Filter
        [HttpPost("service/{serviceId}/filter")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFilter(int serviceId, FilterCreateDTO filterCreateDto)
        {
            if (filterCreateDto.FilterName.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Creating a filter named 'All' is not allowed.");
            }

            try
            {
                var newFilter = _mapper.Map<Filter>(filterCreateDto);
                newFilter.ServiceId = serviceId;

                var createdFilter = await _filterRepository.Insert(newFilter);
                if (createdFilter == null)
                {
                    return BadRequest("Failed to create filter");
                }

                var createdFilterDto = _mapper.Map<FilterDisplayDTO>(createdFilter);
                return CreatedAtAction(nameof(GetFilter), new { id = createdFilter.FilterId }, createdFilterDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update an existing Filter
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFilter(int id, FilterUpdateDTO filterUpdateDto)
        {
            if (filterUpdateDto.FilterName.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Updating a filter to have the name 'All' is not allowed.");
            }

            try
            {
                var filterToUpdate = await _filterRepository.GetDetails(id);
                if (filterToUpdate == null)
                {
                    return NotFound();
                }

                _mapper.Map(filterUpdateDto, filterToUpdate);
                var updatedFilter = await _filterRepository.Update(filterToUpdate);

                var updatedFilterDto = _mapper.Map<FilterDisplayDTO>(updatedFilter);
                return Ok(updatedFilterDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete a filter by id
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFilter(int id)
        {
            try
            {
                var filterToDelete = await _filterRepository.GetDetails(id);

                if (filterToDelete == null)
                {
                    return NotFound("Filter not found");
                }

                if (filterToDelete.FilterName.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("Deleting the 'All' filter is not allowed.");
                }

                if (filterToDelete.Projects.Any())
                {
                    return BadRequest("Cannot delete this filter because it has associated projects.");
                }

                await _filterRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
