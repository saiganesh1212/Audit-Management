using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverity.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuditSeverity.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuditSeverity.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditSeverityController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        private readonly IAuditSeverityProvider _provider;
        
        public AuditSeverityController(IAuditSeverityProvider provider)
        {
            _provider = provider;
            
        }
        [HttpPost]
        public  IActionResult GetAuditResponse(AuditRequest request)
        {
            
            if (ModelState.IsValid)
            {
                AuditResponse auditResult;
                try
                {
                    auditResult = new AuditResponse();
                    auditResult = _provider.GetAuditType(request).Result;
                    _log4net.Info("Audit Response is displayed of project name - " + request.ProjectName);
                    if (auditResult != null)
                    {
                        _log4net.Info("Successfully calculated audit status of project name - " + request.ProjectName);
                        return Ok(auditResult);
                    }
                    else
                    {
                        return StatusCode(404, "Invalid data given");
                    }
                }
                catch (Exception e)
                {
                    _log4net.Error("Some error occured during processing with message - " + e.Message+" for project name "+request.ProjectName);
                    return StatusCode(500, "Some error occured during processing");
                }
            }
            _log4net.Error("Invalid input given by user");
            return StatusCode(400, "Invalid input given");

        }
    }
}
