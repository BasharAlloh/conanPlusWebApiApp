using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.DTOs
{
    public class ServiceDisplayDTO
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<FilterDTO> Filters { get; set; }
        [JsonIgnore]
        public ICollection<Project> Projects { get; set; }  
    }
}
