using System.ComponentModel.DataAnnotations;

namespace conanPlusWebApiApp.Models
{
    public class Partner
    {
        [Key]
        public int PartnerId { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [MaxLength(200)]
        public string ImageFileName { get; set; } 

        [Required(ErrorMessage = "Partner name is required")]
        [MaxLength(100)]
        public string PartnerName { get; set; }
    }
}
