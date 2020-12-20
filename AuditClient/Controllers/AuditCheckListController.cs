using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AuditClient.Models;
using AuditClient.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace AuditClient.Controllers
{
    public class AuditCheckListController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditCheckListController));
        private static AuditRequest _auditRequest;
        private static List<Input> _userInput=new List<Input>();
        private readonly ICheckListProvider _checkListProvider;
        
        public AuditCheckListController(ICheckListProvider checkListProvider)
        {
            _checkListProvider = checkListProvider;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            _log4net.Info("Requesting user input for audittype");
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Index(AuditRequest auditRequest)
        {
            List<string> questions;
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (!ModelState.IsValid)
            {
                return View(auditRequest);
            }

            _auditRequest = auditRequest;

            _log4net.Info("User requested questions of AuditType - " + auditRequest.AuditDetail.AuditType);
            try
            {

                var response = await _checkListProvider.GetQuestions(auditRequest.AuditDetail.AuditType, HttpContext.Session.GetString("token"));
                if (response.Content==null)
                {
                    _log4net.Info("Error in calling AuditCheckListAPI for project name "+auditRequest.ProjectName);
                    ViewBag.error = "Error in calling another service. Please try again";
                    return View(auditRequest);
                }
                var apiResponse = await response.Content.ReadAsStringAsync();
                questions = JsonConvert.DeserializeObject<List<string>>(apiResponse);

                _auditRequest.AuditDetail.Questions = questions;
                if(_userInput.Count!=0)
                {
                    _userInput.Clear();
                }
                foreach (var i in questions)
                {
                    _userInput.Add(new Input() { Question = i, Answer = "" });
                }

                _log4net.Info("AuditType -" + auditRequest.AuditDetail.AuditType + " has " + questions.Count.ToString() + " questions");
                return RedirectToAction("GetAnswers");
            }
            catch(Exception e)
            {
                _log4net.Error("Exception occured with message - " + e.Message+" for project "+auditRequest.ProjectName);
            }
            return View(auditRequest);
            
        }
        
        [HttpGet]
        public ActionResult GetAnswers()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Error = TempData["error"];
            _log4net.Info("Successfully displayed all questions to user - " + HttpContext.Session.GetString("Username"));
            return View(_userInput);
        }
        [HttpPost]
        public ActionResult GetAnswers(IEnumerable<Input> input)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            try
            {

                foreach (var i in input)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(input);

                    }

                }
                

                List<Input> userResponse = input.ToList();
                _auditRequest.AuditDetail.CountOfNos = userResponse.Where(x => x.Answer == "No").Count();
                _log4net.Info(HttpContext.Session.GetString("Username")+" has given "+_auditRequest.AuditDetail.CountOfNos+" answers as NO");
                TempData["AuditRequest"] = JsonConvert.SerializeObject(_auditRequest);

                return RedirectToAction("CalculateSeverity", "AuditSeverity");
            }
            catch (Exception e)
            {
                _log4net.Error("Unexpected error occured with error message - " + e.Message+" of type "+_auditRequest.AuditDetail.AuditType);
                return RedirectToAction("Index");
            }

        }

    }
}