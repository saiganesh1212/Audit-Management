using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthService.Models;
using AuthService.Provider;
using AuthService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        //comment added to test
        //sample
        
        private IAuthProvider _authProvider;
        public AuthController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
            
        }


        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Invalid data given as credentials");
            }
            _log4net.Info("Login Initiated for user " + user.Username);
            try
            {
                string token = _authProvider.GetUser(user);
                if (token == null)
                {
                    _log4net.Info("User does not exist");
                    return StatusCode(404, "User does not exist");
                }
                else
                {

                    _log4net.Info("Successfully logged In and token returned for user " + user.Username);
                    return Ok(new { token = token });
                }
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error occured during login of user " + user.Username+" with message"+e.Message);
                return StatusCode(500, "Unexpected error occured during login");
            }
            
        }
       

    }
}