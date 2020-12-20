using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverity.Models
{
    public class AuditDetail
    {
        public string AuditType { get; set; }
        public DateTime AuditDate { get; set; }
        public List<string> Questions { get; set; }
        public int CountOfNos { get; set; }
    }
}
