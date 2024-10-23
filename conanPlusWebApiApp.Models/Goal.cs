using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conanPlusWebApiApp.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        [MaxLength(300)]
        public string GoalText { get; set; } 
    }

}
