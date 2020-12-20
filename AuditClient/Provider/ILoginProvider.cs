using AuditClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditClient.Provider
{
    public interface ILoginProvider
    {
        Task<HttpResponseMessage> Login(User user);
    }
}
