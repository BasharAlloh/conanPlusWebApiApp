using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;
using conanPlusWebApiApp.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICommonRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmployeeController(ICommonRepository<Employee> employeeRepository, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAll();
            if (employees == null || employees.Count == 0)
            {
                return NotFound("No employees found.");
            }

            var employeeDisplayDtos = _mapper.Map<IEnumerable<EmployeeDisplayDTO>>(employees);
            return Ok(employeeDisplayDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetDetails(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var employeeDisplayDto = _mapper.Map<EmployeeDisplayDTO>(employee);
            return Ok(employeeDisplayDto);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeCreateDTO employeeCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Enum.IsDefined(typeof(EmployeeRole), employeeCreateDto.Role))
            {
                return BadRequest("Invalid role value.");
            }

            try
            {
                var newEmployee = _mapper.Map<Employee>(employeeCreateDto);

                // تأكد من وجود صورة وقم بحفظها
                if (employeeCreateDto.Image != null)
                {
                    newEmployee.ImagePath = await SaveImageAsync(employeeCreateDto.Image, newEmployee.EmployeeId, employeeCreateDto.Name);
                }
                else
                {
                    // في حالة عدم وجود صورة، يمكنك تعيين قيمة افتراضية أو التعامل مع الحقل حسب المتطلبات
                    newEmployee.ImagePath = "uploads/default_employee_image.png"; // قيمة افتراضية على سبيل المثال
                }

                // بعد تعيين ImagePath، قم بإدخال الموظف الجديد في قاعدة البيانات
                var createdEmployee = await _employeeRepository.Insert(newEmployee);
                var employeeDto = _mapper.Map<EmployeeDisplayDTO>(createdEmployee);

                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmployeeId }, employeeDto);
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
        public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeeUpdateDTO employeeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeUpdateDto.EmployeeId)
            {
                return BadRequest("Employee ID mismatch.");
            }

            if (!Enum.IsDefined(typeof(EmployeeRole), employeeUpdateDto.Role))
            {
                return BadRequest("Invalid role value.");
            }

            try
            {
                var existingEmployee = await _employeeRepository.GetDetails(id);
                if (existingEmployee == null)
                {
                    return NotFound("Employee not found.");
                }

                _mapper.Map(employeeUpdateDto, existingEmployee);

                if (employeeUpdateDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(existingEmployee.ImagePath))
                    {
                        DeleteImage(existingEmployee.ImagePath);
                    }

                    existingEmployee.ImagePath = await SaveImageAsync(employeeUpdateDto.Image, existingEmployee.EmployeeId, employeeUpdateDto.Name);
                }
                else
                {
                    existingEmployee.ImagePath = employeeUpdateDto.OldImagePath;
                }

                var updatedEmployee = await _employeeRepository.Update(existingEmployee);
                var employeeDto = _mapper.Map<EmployeeDisplayDTO>(updatedEmployee);
                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetDetails(id);
                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                // حذف الصورة الخاصة بالموظف عند حذفه
                if (!string.IsNullOrEmpty(employee.ImagePath))
                {
                    DeleteImage(employee.ImagePath);
                }

                await _employeeRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<string> SaveImageAsync(IFormFile image, int employeeId, string employeeName)
        {
            var employeeFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "employees", employeeId.ToString());
            if (!Directory.Exists(employeeFolder))
            {
                Directory.CreateDirectory(employeeFolder); 
            }

            var fileName = $"{employeeName}_{DateTime.Now.Ticks}_{image.FileName}";
            var filePath = Path.Combine(employeeFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"uploads/employees/{employeeId}/{fileName}".Replace("\\", "/");
        }

        private void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, imagePath.Replace("/", "\\"));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
