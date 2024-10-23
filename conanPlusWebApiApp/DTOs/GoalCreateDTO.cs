using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class GoalCreateDTO
    {
        [Required]
        [MaxLength(300)]
        public string GoalText { get; set; }
    }
}
