using AuditBenchmark.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuditBenchmark.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuditBenchmarkController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditBenchmarkController));

        private readonly IAuditBenchmarkProvider _auditbenchmarkprovider;

        public AuditBenchmarkController(IAuditBenchmarkProvider auditbenchmarkprovider)
        {
            _auditbenchmarkprovider = auditbenchmarkprovider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _log4net.Info("Http get request initiated ");
                var result = _auditbenchmarkprovider.GetAll();
                if (result != null)
                {
                    _log4net.Info("Successfully got audit benchmark details of "+result.Count+" types");
                    return Ok(result);
                }
                _log4net.Info("BenchMark does not exist");
                return StatusCode(404, "BenchMark does not exist");

            }
            catch (Exception e)
            {
                _log4net.Error("Unexpected error occured" + e.Message);
                return StatusCode(500, "Unexpected error occured");
            }
        }
    }
}

