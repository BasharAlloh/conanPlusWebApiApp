using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class FilterDTO
    {
        [Key]
        public int FilterId { get; set; }
        [Required]
        public string FilterName { get; set; }
    }
}
