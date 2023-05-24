using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Data
{
    public class WebAppfor_AW_workerContext : DbContext
    {
        public WebAppfor_AW_workerContext (DbContextOptions<WebAppfor_AW_workerContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppfor_AW_worker.Models.ComplaintTbl> ComplaintTbl { get; set; } = default!;
        public DbSet<WebAppfor_AW_worker.Models.UserTbl> UserTbl { get; set; } = default!;
        public DbSet<WebAppfor_AW_worker.Models.WorkerTbl> WorkerTbl { get; set; } = default!;
        public DbSet<WebAppfor_AW_worker.Models.JobTbl> JobTbl { get; set; } = default!;
        public DbSet<WebAppfor_AW_worker.Models.RegionTbl> RegionTbl { get; set; } = default!;
        public DbSet<WebAppfor_AW_worker.Models.RequestTbl> RequestTbl { get; set; } = default!;
    }
}
