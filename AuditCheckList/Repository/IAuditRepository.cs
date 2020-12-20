using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.Repository
{
    public interface IAuditRepository
    {
        public List<string> GetByType(string auditType);
    }
}
