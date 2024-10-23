using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class VisionCreateDTO
    {
        [Required]
        public string Description { get; set; }
    }
}
