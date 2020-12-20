using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Models
{
    public class JWT
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
