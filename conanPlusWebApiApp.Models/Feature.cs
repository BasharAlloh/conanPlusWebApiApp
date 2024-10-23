using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; } 


    }   


}
