using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class EmployeeUpdateDTO
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        public IFormFile Image { get; set; } 

        [Required]
        [RegularExpression("Hr|Employee", ErrorMessage = "Role must be either 'Hr' or 'Employee'")]
        public string Role { get; set; }

        public string OldImagePath { get; set; } 
    }
}
