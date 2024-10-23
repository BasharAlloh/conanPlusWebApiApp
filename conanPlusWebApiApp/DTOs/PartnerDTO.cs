using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.DTOs
{
    public class PartnerDTO
    {
        [Key]
        public int PartnerId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImageFileName { get; set; }

        [Required]
        [MaxLength(100)]
        public string PartnerName { get; set; }
    }
}
