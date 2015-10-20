using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WindsorExample.Models;

namespace WindsorExample.DataAccessLayer
{
    public class LinkContext:DbContext
    {
        public LinkContext() : base("LinkContext")
        {
        }

        public DbSet<ReportViewModel> Reports { get; set; }//can omit because Links creates
        public DbSet<KpiViewModel> Kpis { get; set; }//can omit because Links creates
        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}