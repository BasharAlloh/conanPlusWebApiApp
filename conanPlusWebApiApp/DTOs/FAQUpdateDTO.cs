using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class FAQUpdateDTO
    {
        [Key]
        public int FAQId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Question { get; set; }

        [Required]
        [MaxLength(500)]
        public string Answer { get; set; }
    }
}
