using System;
using System.Data.Entity;
using System.Diagnostics;
using NinjaDomain.Classes;

namespace NinjaDomain.DataModel
{
    public class NinjaContext:DbContext
    {
        /**[InvalidOperationException: This operation requires a connection to the 'master' database]:
        By default, we check if the database you are trying to use exists the first time that the context is used in an AppDomain.  This check requires a connection to the master database, which it appears is not possible with the connection you are using.  We will look into making this experience better.  For now, try adding the following call at some point in your application initialization (before you use the DbContext for the first time):
            Database.SetInitializer<ProfileCatalog>(null);
         This will tell Magic Unicorn to not attempt any initialization, including skipping the check for whether or not the database exists.**/

        public NinjaContext():base("name=DefaultConnection")
        {
            Console.WriteLine("InContextConstructor");
            Console.ReadLine();
            Database.SetInitializer<NinjaContext>(null);
        }
        public DbSet<Ninja> Ninjas { get; set; } 
        public DbSet<Clan> Clans { get; set; }
        public DbSet<NinjaEquipment> Equipment { get; set; }  
    }
}
