using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "Package title is required")]
        [MaxLength(100)] 
        public string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } 

        [Required(ErrorMessage = "Details are required")]
        [MaxLength(500)] 
        public string Details { get; set; }

        [MaxLength(100)] 
        public string WhatsAppLink { get; set; }
    }

}
