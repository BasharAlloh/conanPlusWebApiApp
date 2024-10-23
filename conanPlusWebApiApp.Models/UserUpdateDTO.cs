using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.Models
{
    public class UserUpdateDTO
    {
        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }
    }
}
