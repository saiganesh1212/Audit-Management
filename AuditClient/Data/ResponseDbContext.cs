using AuditClient.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditClient.Data
{
    public class ResponseDbContext : DbContext
    {
        public ResponseDbContext(DbContextOptions<ResponseDbContext> options) : base(options)
        {

        }
        public virtual DbSet<AuditResponseDbo> AuditResponse { get; set; }
    }
}
