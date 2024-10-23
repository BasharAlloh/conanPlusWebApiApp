using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class Vision
    {
        [Key]
        public int VisionId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } // الوصف
    }


}
