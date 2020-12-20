using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Models
{
    public class AuditRequest
    {
        [Required(ErrorMessage ="Please enter project name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Please enter project manager name")]
        public string ProjectManagerName { get; set; }

        [Required(ErrorMessage = "Please enter application owner name")]
        public string ApplicationOwnerName { get; set; }

        public AuditDetail AuditDetail { get; set; }
    }
}
