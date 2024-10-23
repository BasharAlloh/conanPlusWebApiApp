using conanPlusWebApiApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Dal
{
    public class ProjectRepository : CommonRepository<Project>, IProjectRepository
    {
        private readonly conanPlusWebApiAppDbContext _context;

        public ProjectRepository(conanPlusWebApiAppDbContext context) : base(context)
        {
            _context = context;
        }

        // Get projects by service id with related data
        public async Task<List<Project>> GetProjectsWithRelatedDataByServiceId(int serviceId)
        {
            return await _context.Projects
                                 .Include(p => p.Service)
                                 .Include(p => p.Filter)
                                 .Where(p => p.ServiceId == serviceId)
                                 .ToListAsync();
        }

        // Get projects by service id (as defined in the interface)
        public async Task<List<Project>> GetProjectsByServiceId(int serviceId)
        {
            return await _context.Projects
                                 .Where(p => p.ServiceId == serviceId)
                                 .ToListAsync();
        }

        // Get all projects with related data
        public async Task<List<Project>> GetAllProjectsWithRelatedData()
        {
            return await _context.Projects
                                 .Include(p => p.Service)
                                 .Include(p => p.Filter)
                                 .ToListAsync();
        }

        // Update project images
        public async Task UpdateProjectImages(int projectId, string externalImagePath, string internalImagePath)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                // Delete old images from the server
                if (!string.IsNullOrEmpty(project.ExternalImageUrl))
                    File.Delete(project.ExternalImageUrl);
                if (!string.IsNullOrEmpty(project.InternalImageUrl))
                    File.Delete(project.InternalImageUrl);

                // Update paths in the database
                project.ExternalImageUrl = externalImagePath;
                project.InternalImageUrl = internalImagePath;

                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        // Delete project and images
        public async Task DeleteProjectAndImages(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                // Delete images from the server
                if (!string.IsNullOrEmpty(project.ExternalImageUrl))
                    File.Delete(project.ExternalImageUrl);
                if (!string.IsNullOrEmpty(project.InternalImageUrl))
                    File.Delete(project.InternalImageUrl);

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}
