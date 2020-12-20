using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditClient.Helpers
{
    public interface IClientAddress
    {
        HttpClient GetAuthServiceAddress();
        HttpClient GetCheckListAddress();
        HttpClient GetSeverityAddress();
    }
}
