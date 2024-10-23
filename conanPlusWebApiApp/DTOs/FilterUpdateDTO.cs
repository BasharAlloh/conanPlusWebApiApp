using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class FilterUpdateDTO
    {
        [Key]
        public int FilterId { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression("^(?!All$).*$", ErrorMessage = "The filter name 'All' is reserved and cannot be used.")]
        public string FilterName { get; set; }

        public int ServiceId { get; set; }
    }
}
    