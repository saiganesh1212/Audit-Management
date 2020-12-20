using AuditCheckList.Provider;
using AuditCheckList.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditCheckListController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditCheckListController));
        private readonly IAuditProvider _provider;
        public AuditCheckListController(IAuditProvider provider)
        {
            _provider = provider;
        }
        [HttpGet("{AuditType}")]
        public IActionResult Get(string AuditType)
        {
            try
            {
                _log4net.Info("Http get request initiated with " + AuditType);
                var questionlist = _provider.GetList(AuditType);
                if (questionlist == null)
                {
                    _log4net.Error("No content Obtained ");
                    return StatusCode(404,"No questions are present");
                }
                _log4net.Info("Successfully returned the questions of type -" + AuditType);
                 return Ok(questionlist);
            }
            catch(Exception e)
            {
                _log4net.Error("Error in fetching details " + e.Message);
                return StatusCode(500,"Error in fetching details");
            }
        }
    }
}
