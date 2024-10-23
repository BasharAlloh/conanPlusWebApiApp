using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProjectTitle { get; set; }

        [MaxLength(300)]
        public string ExternalImageUrl { get; set; }

        [MaxLength(300)]
        public string InternalImageUrl { get; set; }

        [MaxLength(300)]
        public string ProjectLink { get; set; }

        // Foreign key for Service
        public int ServiceId { get; set; }

        // Foreign key for Filter
        public int FilterId { get; set; }

        // Navigation properties
        public Service Service { get; set; }
        public Filter Filter { get; set; }
    }
}
