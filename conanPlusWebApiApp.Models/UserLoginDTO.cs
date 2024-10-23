using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.Models
{
    public class UserLoginDTO
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
