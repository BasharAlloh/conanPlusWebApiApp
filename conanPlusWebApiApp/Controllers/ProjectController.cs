using AutoMapper;
using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ICommonRepository<Project> _projectRepository;
        private readonly ICommonRepository<Filter> _filterRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProjectController(ICommonRepository<Project> projectRepository, ICommonRepository<Filter> filterRepository, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _projectRepository = projectRepository;
            _filterRepository = filterRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        // Get a project by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectRepository.GetDetails(id);
            if (project == null)
            {
                return NotFound("Project not found");
            }

            var projectDto = _mapper.Map<ProjectDisplayDTO>(project);
            return Ok(projectDto);
        }

        // Get all projects by service ID
        [HttpGet("service/{serviceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectsByService(int serviceId)
        {
            var projects = (await _projectRepository.GetAll())
                .Where(p => p.ServiceId == serviceId)
                .ToList();

            if (projects == null || projects.Count == 0)
            {
                return NotFound("No projects found for the specified service.");
            }

            var projectDtos = _mapper.Map<IEnumerable<ProjectDisplayDTO>>(projects);
            return Ok(projectDtos);
        }

        // Create a new project
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProject([FromForm] ProjectCreateDTO projectCreateDto)
        {
            if (projectCreateDto == null)
            {
                return BadRequest("Invalid project data");
            }

            try
            {
                var newProject = _mapper.Map<Project>(projectCreateDto);

                // Handle file uploads
                if (projectCreateDto.ExternalImage != null)
                {
                    newProject.ExternalImageUrl = await SaveFileAsync(projectCreateDto.ExternalImage, projectCreateDto.ProjectTitle, "external");
                }

                if (projectCreateDto.InternalImage != null)
                {
                    newProject.InternalImageUrl = await SaveFileAsync(projectCreateDto.InternalImage, projectCreateDto.ProjectTitle, "internal");
                }

                var createdProject = await _projectRepository.Insert(newProject);
                var createdProjectDto = _mapper.Map<ProjectDisplayDTO>(createdProject);

                return CreatedAtAction(nameof(GetProject), new { id = createdProject.ProjectId }, createdProjectDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProject(int projectId, [FromForm] ProjectUpdateDTO projectUpdateDto)
        {
            var projectToUpdate = await _projectRepository.GetDetails(projectId);
            if (projectToUpdate == null)
            {
                return NotFound("Project not found");
            }

            try
            {
                // تحديث الحقول الأساسية
                projectToUpdate.ProjectTitle = projectUpdateDto.ProjectTitle;
                projectToUpdate.ProjectLink = projectUpdateDto.ProjectLink;
                projectToUpdate.ServiceId = projectUpdateDto.ServiceId;
                projectToUpdate.FilterId = projectUpdateDto.FilterId;

                // التعامل مع الصور: الاحتفاظ بالصور القديمة إذا لم يتم تحميل صور جديدة
                if (projectUpdateDto.ExternalImage != null)
                {
                    projectToUpdate.ExternalImageUrl = await SaveFileAsync(projectUpdateDto.ExternalImage, projectUpdateDto.ProjectTitle, "external");
                }

                if (projectUpdateDto.InternalImage != null)
                {
                    projectToUpdate.InternalImageUrl = await SaveFileAsync(projectUpdateDto.InternalImage, projectUpdateDto.ProjectTitle, "internal");
                }

                // تحديث المشروع في قاعدة البيانات
                var updatedProject = await _projectRepository.Update(projectToUpdate);
                var updatedProjectDto = _mapper.Map<ProjectDisplayDTO>(updatedProject);

                return Ok(updatedProjectDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        // Delete a project by ID
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var projectToDelete = await _projectRepository.GetDetails(id);
            if (projectToDelete == null)
            {
                return NotFound("Project not found");
            }

            try
            {
                // Delete associated images
                DeleteFile(projectToDelete.ExternalImageUrl);
                DeleteFile(projectToDelete.InternalImageUrl);

                await _projectRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Save a file to the server
        private async Task<string> SaveFileAsync(IFormFile file, string projectTitle, string imageType)
        {
            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", $"project-{projectTitle}");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var filePath = Path.Combine(uploadsFolderPath, $"{imageType}-{file.FileName}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"uploads/project-{projectTitle}/{imageType}-{file.FileName}".Replace("\\", "/");
        }

        // Delete a file from the server
        private void DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, filePath.Replace("/", "\\"));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
        }
    }
}
