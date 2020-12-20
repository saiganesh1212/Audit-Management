using AuditSeverity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverity.Repository
{
   public  interface IAuditSeverityRepository
   {
        public Task<AuditResponse> GetBenchMarkValues(AuditRequest request);
   }
}
