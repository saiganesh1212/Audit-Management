using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverity.Models
{
    public class AuditResponse
    {
        [Key]
        public int AuditId { get; set; }
        public string ProjectExecutionStatus { get; set; }
        public string RemedialActionDuration { get; set; }
    }
}
