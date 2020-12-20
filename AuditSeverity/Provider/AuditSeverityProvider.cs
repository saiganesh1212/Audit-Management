using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditSeverity.Models;
using AuditSeverity.Repository;
using Newtonsoft.Json;
namespace AuditSeverity.Provider
{
    public class AuditSeverityProvider: IAuditSeverityProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityProvider));
        private IAuditSeverityRepository _repository;
        public AuditSeverityProvider(IAuditSeverityRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuditResponse> GetAuditType(AuditRequest request)
        {
            AuditResponse response = new AuditResponse();
            try
            {
                response = await _repository.GetBenchMarkValues(request);
                _log4net.Info("Successfully got the response from benchmark service having id "+response.AuditId);
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured with message " + e.Message);
                return null;
            }
            return response;
            
        }
    }
}
