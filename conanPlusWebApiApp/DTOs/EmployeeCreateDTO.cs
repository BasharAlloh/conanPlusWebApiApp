using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class EmployeeCreateDTO
    {
        [Required(ErrorMessage = "Employee name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [MaxLength(100)]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }  

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("Hr|Employee", ErrorMessage = "Role must be either 'Hr' or 'Employee'")]
        public string Role { get; set; }
    }
}
