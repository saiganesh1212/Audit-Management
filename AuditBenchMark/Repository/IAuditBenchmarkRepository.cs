using System.Collections.Generic;

namespace AuditBenchmark.Repository
{
    public interface IAuditBenchmarkRepository
    {
        public List<Models.AuditBenchmark> GetAll();
    }
}
