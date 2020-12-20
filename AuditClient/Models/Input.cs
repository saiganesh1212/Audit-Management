using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Models
{
    public class Input
    {
        [Required]
        public string Question { get; set; }
        [Required(ErrorMessage ="Please select option")]
        public string Answer { get; set; }
    }
}
