using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class VisionUpdateDTO
    {
        [Key]
        public int VisionId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
