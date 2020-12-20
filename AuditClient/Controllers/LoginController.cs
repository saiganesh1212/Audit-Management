using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuditClient.Models;
using AuditClient.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuditClient.Controllers
{
    public class LoginController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        private readonly ILoginProvider _loginProvider;
        public LoginController(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;
        }
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            _log4net.Info("User Login Initiated of name "+user.Username);
            
            try
            {
                var response1 =await  _loginProvider.Login(user);
                if (!response1.IsSuccessStatusCode)
                {
                    ViewBag.message = "Invalid Login Credentials";
                    _log4net.Info("Invalid user details are given with name" + user.Username);
                    return View(user);
                }

                        string stringJWT = await response1.Content.ReadAsStringAsync();
                        var jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                        _log4net.Info(user.Username + " successfully logged in");
                        HttpContext.Session.SetString("token", jwt.Token);
                        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                        HttpContext.Session.SetString("Username", user.Username);

                        return RedirectToAction("Index", "AuditCheckList");
                
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error occured in Login Controller with message -" + e.Message);
                return View(user);
            }
            
        }

        public ActionResult Logout()
        {
            _log4net.Info(HttpContext.Session.GetString("Username") + " Successfully logged out");
            HttpContext.Session.Remove("token");
            // HttpContext.Session.SetString("user", null);

            HttpContext.Session.Clear();
            
            return View("Login");
        }
    }
}