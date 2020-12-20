using AuditClient.Helpers;
using AuditClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public class LoginProvider : ILoginProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginProvider));
        private readonly IClientAddress _address;
        public LoginProvider(IClientAddress address)
        {
            _address = address;
        }
        public async Task<HttpResponseMessage> Login(User user)
        {
            HttpResponseMessage response=new HttpResponseMessage();
            StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            try
            {
                using (var httpClient = _address.GetAuthServiceAddress())
                {
                    response = await httpClient.PostAsync("api/auth/login", content1);
                    //response = await httpClient.PostAsync("https://localhost:44311/api/auth/login", content1);
                }
                _log4net.Info("Successfully called login service with username "+user.Username);
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured in Login Provider with message  " + e.Message+" for user "+user.Username);
            }
            return response;

        }
    }
}
