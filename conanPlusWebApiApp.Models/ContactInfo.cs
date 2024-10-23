using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class ContactInfo
    {
        [Key]
        public int ContactId { get; set; }

        [MaxLength(200)] 
        public string Address { get; set; }

        [MaxLength(15)] 
        public string Phone { get; set; }

        [MaxLength(50)] 
        public string WorkingHours { get; set; }

        [MaxLength(100)] 

        public string Email { get; set; }

        [MaxLength(15)] 
        public string WhatsApp { get; set; }

        [MaxLength(100)] 
        public string Instagram { get; set; }
    }

}
