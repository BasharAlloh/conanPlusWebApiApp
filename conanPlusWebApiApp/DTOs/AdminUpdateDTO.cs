using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class AdminUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain a mix of uppercase, lowercase, numbers, and special characters.")]
        public string Password { get; set; }
    }
}
