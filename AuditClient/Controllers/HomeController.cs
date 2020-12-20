using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using AuditClient.Models;

namespace AuditClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _log4net.Info("Homepage is displayed to user");
            return View();
        }

        public IActionResult Privacy()
        {
            _log4net.Info("Privacypage is displayed to user");
            return View();
        }
        public IActionResult AuditManagement()
        {
            _log4net.Info("AuditManagement page is displayed to user");
            return View();
        }
        public IActionResult ManagingAudit()
        {
            _log4net.Info("ManagingAudit page is displayed to user");
            return View();
        }
        public IActionResult AuditDirective()
        {
            _log4net.Info("AuditDirective page is displayed to user");
            return View();
        }
    }
}
