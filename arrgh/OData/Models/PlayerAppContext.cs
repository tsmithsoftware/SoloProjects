using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OData.Models
{
    public class PlayerAppContext:DbContext
    {
        public PlayerAppContext() : base("name=PlayerAppContext")
        {
        }
        public DbSet<Player> Players { get; set; }
    }
}