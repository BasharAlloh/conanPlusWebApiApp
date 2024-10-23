using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class CustomServiceBtnLink
    {
        [Key]
        public int CustomServiceId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Link { get; set; }
    }
}
