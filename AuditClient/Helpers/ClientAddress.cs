using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditClient.Helpers
{
    public class ClientAddress:IClientAddress
    {
        HttpClient client;
        private IConfiguration _config;
        public ClientAddress(IConfiguration configuration)
        {
            client = new HttpClient();
            _config = configuration;
        }
        public HttpClient GetAuthServiceAddress()
        {
            client.BaseAddress = new Uri(_config["AuthServiceUrl"]);
            return client;
        }
        public HttpClient GetCheckListAddress()
        {
            client.BaseAddress = new Uri(_config["CheckListServiceUrl"]);
            return client;
        }
        public HttpClient GetSeverityAddress()
        {
            client.BaseAddress = new Uri(_config["SeverityServiceUrl"]);
            return client;
        }
    }
}
