using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [MaxLength(100)]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [MaxLength(200)]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public EmployeeRole Role { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now; 
    }


}
