using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class AboutUs
    {
        [Key]
        public int AboutUsId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } 

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } 

    }



}
