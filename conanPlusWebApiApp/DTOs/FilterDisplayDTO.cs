using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class FilterDisplayDTO
    {
        [Key]
        public int FilterId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FilterName { get; set; }

        [Required]
        public string ServiceName { get; set; }
        [Required]
        public List<string> ProjectNames { get; set; } = new List<string>();
    }
}
