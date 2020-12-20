using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Models
{
    public class AuditDetail
    {
        [Required(ErrorMessage ="Please select audit type")]
        public string AuditType { get; set; }
        [Required(ErrorMessage ="Please select audit date")]
        public DateTime AuditDate { get; set; }
        public List<string> Questions { get; set; }
        public int CountOfNos { get; set; }
    }
}
