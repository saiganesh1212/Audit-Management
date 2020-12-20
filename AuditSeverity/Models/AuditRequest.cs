using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverity.Models
{
    public class AuditRequest
    {

        public string ProjectName { get; set; }
        public string ProjectManagerName { get; set; }
        public string ApplicationOwnerName { get; set; }
        public AuditDetail AuditDetail { get; set; }
        
    }
}
