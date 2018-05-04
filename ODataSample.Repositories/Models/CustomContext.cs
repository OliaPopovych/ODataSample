using ODataSample.Repositories.Models;
using System.Data.Entity;

namespace ODataSample.Models
{
    public class CustomContext : DbContext
    {
        public CustomContext() : base("name=CustomConnectionString")
        {
            Database.SetInitializer<CustomContext>(new CustomInitializer());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}