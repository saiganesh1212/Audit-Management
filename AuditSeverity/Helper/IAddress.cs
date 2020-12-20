using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditSeverity.Helper
{
    public interface IAddress
    {
        HttpClient GetBenchmarkAddress();

    }
}
