using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class FAQ
    {
        [Key]
        public int FAQId { get; set; }

        [Required(ErrorMessage = "Question is required")]
        [MaxLength(200)] 
        public string Question { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        [MaxLength(500)] 
        public string Answer { get; set; }
    }

}
