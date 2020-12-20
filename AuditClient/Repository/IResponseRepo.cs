using AuditClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Repository
{
    public interface IResponseRepo
    {
        Task<bool> CreateResponse(AuditResponseDbo responseDbo);
    }
}
