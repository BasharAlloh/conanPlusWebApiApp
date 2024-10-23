using Microsoft.AspNetCore.Http;

namespace conanPlusWebApiApp.DTOs
{
    public class ProjectCreateDTO
    {
        public string ProjectTitle { get; set; }
        public string ProjectLink { get; set; }
        public int ServiceId { get; set; }
        public int FilterId { get; set; }

        public IFormFile ExternalImage { get; set; }
        public IFormFile InternalImage { get; set; }
    }
}
