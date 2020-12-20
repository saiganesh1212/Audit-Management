using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.Provider
{
    public interface IAuditProvider
    {
        public List<string> GetList(string AuditType);
    }
}
