using System.Data.Entity;
namespace EntityApp.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext()
                : base("name=ProductsContext")
        {
            //In the constructor, "name=ProductsContext" gives the name of the connection string.
        }
        public DbSet<Product> Products { get; set; }
    }
}