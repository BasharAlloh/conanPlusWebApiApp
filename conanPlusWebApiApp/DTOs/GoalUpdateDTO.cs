using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class GoalUpdateDTO
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        [MaxLength(300)]
        public string GoalText { get; set; }
    }
}
