using AuditClient.Data;
using AuditClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Repository
{
    public class ResponseRepo : IResponseRepo
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ResponseRepo));
        private readonly ResponseDbContext _context;
        public ResponseRepo(ResponseDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateResponse(AuditResponseDbo auditResponse)
        {
            try
            {
                
                _context.AuditResponse.Add(auditResponse);
                await _context.SaveChangesAsync();
                _log4net.Info("Repository layer has added the response into database");
                return true;
            }
            catch (Exception e)
            {
                _log4net.Error("Unexpected error has occured");
                return false;
            }


        }
    }
}
