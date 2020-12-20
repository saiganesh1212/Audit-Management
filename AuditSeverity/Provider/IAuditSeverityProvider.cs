using AuditSeverity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverity.Provider
{
    public interface IAuditSeverityProvider
    {
        public Task<AuditResponse> GetAuditType(AuditRequest request);
    }
}
