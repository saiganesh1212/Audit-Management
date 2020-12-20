using AuditClient.Helpers;
using AuditClient.Models;
using AuditClient.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public class SeverityProvider : ISeverityProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SeverityProvider));
        private readonly IResponseRepo _response;
        private readonly IClientAddress _address;
        public SeverityProvider(IResponseRepo response,IClientAddress address)
        {
            _response = response;
            _address = address;
        }
        public async Task<HttpResponseMessage> CalculateSeverity(AuditRequest request,string token)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            try
            {
                using (var client = _address.GetSeverityAddress())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
                    response = await client.PostAsync("api/auditseverity/", content);
                  //  response = await client.PostAsync("https://localhost:44316/api/auditseverity/", content);
                    _log4net.Info("Successfully got the response in severity provider for project " + request.ProjectName);
                }
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured with message " + e.Message + " for request with project name " + request.ProjectName);
            }
            return response;

        }

        public async Task<bool> CreateResponse(AuditResponseDbo auditResponse)
        {
            bool result;
            try
            {
                _log4net.Info("Provider called repository to store data for response with id " + auditResponse.AuditId);
                result =await _response.CreateResponse(auditResponse);
                
                return result;

            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error occured with message " + e.Message + " for response with id " + auditResponse.AuditId);
                return false;
            }
               
        }
    }
}
