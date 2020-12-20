using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public interface ICheckListProvider
    {
        Task<HttpResponseMessage> GetQuestions(string AuditType,string token);
    }
}
