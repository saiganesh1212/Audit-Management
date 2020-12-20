using AuditBenchmark.Repository;
using System;
using System.Collections.Generic;

namespace AuditBenchmarkService.Repository
{
    public class AuditBenchmarkRepository : IAuditBenchmarkRepository
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditBenchmarkRepository));
        private List<AuditBenchmark.Models.AuditBenchmark> answers;

        public AuditBenchmarkRepository()
        {
            answers = new List<AuditBenchmark.Models.AuditBenchmark>{
                new AuditBenchmark.Models.AuditBenchmark { AuditType = "Internal" , BenchmarkNoAnswers = 3},
                new AuditBenchmark.Models.AuditBenchmark { AuditType = "SOX" , BenchmarkNoAnswers = 1},

            };
        }
        public List<AuditBenchmark.Models.AuditBenchmark> GetAll()
        {
            try
            {
                _log4net.Info("Benchmark Repository has been requested");
                return answers;
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured in repository with message -" + e.Message);
                return null;
            }
            
        }

    }
}










