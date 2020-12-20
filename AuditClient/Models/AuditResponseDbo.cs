using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Models
{
    public class AuditResponseDbo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime DateofExecution { get; set; }

        [Required]
        public int AuditId { get; set; }
        [Required]
        public string ProjectExecutionStatus { get; set; }
        [Required]
        public string RemedialActionDuration { get; set; }
    }
}
