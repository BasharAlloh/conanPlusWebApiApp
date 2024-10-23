using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } 

        // Navigation property for related filters
        public ICollection<Filter> Filters { get; set; }

        // Navigation property for related projects
        public ICollection<Project> Projects { get; set; }
    }
}
