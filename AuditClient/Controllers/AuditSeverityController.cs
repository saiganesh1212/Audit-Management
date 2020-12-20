using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditClient.Models;
using AuditClient.Provider;
using AuditClient.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuditClient.Controllers
{
    public class AuditSeverityController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        private readonly ISeverityProvider _severityProvider;
        
        public AuditSeverityController(ISeverityProvider severityProvider)
        {
            _severityProvider = severityProvider;
        }
        [HttpGet]
        public async Task<IActionResult> CalculateSeverity()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            AuditRequest auditRequest = JsonConvert.DeserializeObject<AuditRequest>(TempData["AuditRequest"].ToString());

            try
            {

                var response = await _severityProvider.CalculateSeverity(auditRequest, HttpContext.Session.GetString("token"));
                if (response.Content == null)
                {
                    TempData["error"] = "Unable to call other Sevice";
                    return RedirectToAction("GetAnswers", "AuditCheckList");
                }
                _log4net.Info(HttpContext.Session.GetString("Username") + " has successfully calculated severity");
                var apiResponse = await response.Content.ReadAsStringAsync();
                AuditResponse auditResponse = JsonConvert.DeserializeObject<AuditResponse>(apiResponse);
                _log4net.Info("Audit with id - " + auditResponse.AuditId + " has status " + auditResponse.ProjectExecutionStatus);

                AuditResponseDbo responseDbo = new AuditResponseDbo();
                responseDbo.Username = HttpContext.Session.GetString("Username");
                responseDbo.DateofExecution = DateTime.Now;
                responseDbo.AuditId = auditResponse.AuditId;
                responseDbo.ProjectExecutionStatus = auditResponse.ProjectExecutionStatus;
                responseDbo.RemedialActionDuration = auditResponse.RemedialActionDuration;
                if (_severityProvider.CreateResponse(responseDbo).Result == true)
                {
                    _log4net.Info("Successfully entered response in database of id "+responseDbo.AuditId);
                }
                else
                {
                    _log4net.Error("Some error occured in storing in database of id "+responseDbo.AuditId);
                }
                return View(auditResponse);
            }
            catch (Exception e)
            {
                _log4net.Error("Unexpected error has occured with message -" + e.Message+" for project name "+auditRequest.ProjectName);
                return RedirectToAction("Index", "AuditCheckList");
            }


        }
    }
}