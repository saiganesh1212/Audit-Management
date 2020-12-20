using AuthService.Models;
using AuthService.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Provider
{
    public class AuthProvider : IAuthProvider
    {
        private IConfiguration _config;
        private IAuthRepo _authRepo;
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthProvider));
        public AuthProvider(IAuthRepo repo,IConfiguration configuration)
        {
            _authRepo = repo;
            _config = configuration;
        }
        public string GetUser(User userDetails)
        {
            bool result;
            try
            {
                result= _authRepo.Login(userDetails);
                if (!result)
                {
                    _log4net.Info("User with given credentials does not exist");
                    return null;
                }
                return GenerateJSONWebToken(userDetails);

            }
            catch (Exception e)
            {
                _log4net.Info("Error in the process of login"+e.Message);
                return null;
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            _log4net.Info("Token Generation Started");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }
    }
}
