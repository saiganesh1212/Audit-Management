
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditSeverity.Helper
{
    public class Address : IAddress
    {
        HttpClient client;
        private IConfiguration _config;
        public Address(IConfiguration configuration)
        {
            client = new HttpClient();
            _config = configuration;
        }
        public HttpClient GetBenchmarkAddress()
        {
            client.BaseAddress = new Uri(_config["BenchmarkUrl"]);
            return client;
        }
    }
}
