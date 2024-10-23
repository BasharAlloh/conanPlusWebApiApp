using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace conanPlusWebApiApp.Models
{
    public class Filter
    {
        [Key]
        public int FilterId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FilterName { get; set; }

        // Foreign key for Service
        public int ServiceId { get; set; }


        // Navigation property for related service
        [JsonIgnore]

        public Service Service { get; set; }

        // Navigation property for related projects
        public ICollection<Project> Projects { get; set; }
    }
}
