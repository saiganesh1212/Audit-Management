using AuditClient.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public class CheckListProvider : ICheckListProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CheckListProvider));
        private readonly IClientAddress _address;
        public CheckListProvider(IConfiguration config,IClientAddress clientAddress)
        {
            _address = clientAddress;
        }
        public async Task<HttpResponseMessage> GetQuestions(string AuditType,string token)
        {
            HttpResponseMessage response=new HttpResponseMessage();
            try
            {
                using (var client = _address.GetCheckListAddress())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
                    response = await client.GetAsync("api/auditchecklist/" + AuditType);
                    //response = await client.GetAsync("https://localhost:44320/api/auditchecklist/" + AuditType);
                    _log4net.Info("Checklist provider has successfully got questions of type " + AuditType);

                }
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured with message -" + e.Message+" of type "+AuditType);
            }
            return response;
        }
    }
}
