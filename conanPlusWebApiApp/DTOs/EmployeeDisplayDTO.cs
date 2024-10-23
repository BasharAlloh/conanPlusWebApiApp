using conanPlusWebApiApp.Models;
using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class EmployeeDisplayDTO
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImagePath { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
