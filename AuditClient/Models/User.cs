using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="Please enter name")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Please enter password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
