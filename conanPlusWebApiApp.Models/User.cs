using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(500)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        public int TokenVersion { get; set; } = 1;
    }
}
