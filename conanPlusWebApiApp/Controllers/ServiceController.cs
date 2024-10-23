using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using conanPlusWebApiApp.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ICommonRepository<Service> _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceController(ICommonRepository<Service> serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                var services = await _serviceRepository.GetAll();
                var serviceDtos = _mapper.Map<IEnumerable<ServiceDisplayDTO>>(services);
                return Ok(serviceDtos);
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
        public async Task<IActionResult> GetServiceById(int id)
        {
            try
            {
                var service = await _serviceRepository.GetDetails(id);
                if (service == null)
                {
                    return NotFound("Service not found");
                }

                var serviceDto = _mapper.Map<ServiceDisplayDTO>(service);
                return Ok(serviceDto);
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
        public async Task<IActionResult> UpdateService(int id, ServiceUpdateDTO serviceUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceUpdateDto.ServiceId)
            {
                return BadRequest("Service ID mismatch");
            }

            try
            {
                var existingService = await _serviceRepository.GetDetails(id);
                if (existingService == null)
                {
                    return NotFound("Service not found");
                }

                existingService.ServiceName = serviceUpdateDto.ServiceName;
                existingService.Description = serviceUpdateDto.Description;

                var updatedService = await _serviceRepository.Update(existingService);
                var serviceDto = _mapper.Map<ServiceDisplayDTO>(updatedService);

                return Ok(serviceDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
