using System.Collections.Generic;

namespace AuditBenchmark.Provider
{
    public interface IAuditBenchmarkProvider
    {
        List<Models.AuditBenchmark> GetAll();
    }
}