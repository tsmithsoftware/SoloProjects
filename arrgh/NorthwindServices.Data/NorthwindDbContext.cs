using NorthwindServices.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NorthwindServices.Data
{
    public class NorthwindDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }    
}
