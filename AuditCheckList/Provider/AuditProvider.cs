using AuditCheckList.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.Provider
{
    public class AuditProvider:IAuditProvider
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditProvider));
        private readonly IAuditRepository _service;
        public AuditProvider(IAuditRepository service)
        {
            _service = service;
        }
        public List<string> GetList(string AuditType)
        {
            if (AuditType == null || AuditType == "" || (AuditType != "Internal" && AuditType != "SOX"))
            {
                return null;
            }
            try
            {
                var result= _service.GetByType(AuditType);
                _log4net.Info("Successfully got checklist from repo layer of type "+AuditType);
                return result;
            }
            catch(Exception e)
            {
                _log4net.Error("Unexpected error has occured in provider layer with message " + e.Message+" while requesting type "+AuditType);
                return null;
            }
            
        }

    }
}
