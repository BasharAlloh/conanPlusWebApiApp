using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class FilterCreateDTO
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression("^(?!All$).*$", ErrorMessage = "The filter name 'All' is reserved and cannot be used.")]
        public string FilterName { get; set; }
        [Required]

        public int ServiceId { get; set; }
    }
}
