using AuditBenchmark.Repository;
using System;
using System.Collections.Generic;

namespace AuditBenchmark.Provider
{
    public class AuditBenchmarkProvider : IAuditBenchmarkProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditBenchmarkProvider));
        private readonly IAuditBenchmarkRepository _auditbenchmarkservice;
        public AuditBenchmarkProvider(IAuditBenchmarkRepository auditbenchmarkservice)
        {
            _auditbenchmarkservice = auditbenchmarkservice;
        }

        public List<Models.AuditBenchmark> GetAll()
        {
            try
            {
                _log4net.Info("Provider layer has been called");
                return _auditbenchmarkservice.GetAll();
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured with message - " + e.Message);
                return null;
            }
            
        }


    }
}