using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class ServiceUpdateDTO
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
