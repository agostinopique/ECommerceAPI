using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Models
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext() { }

        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localt;Initial Catalog=ECommerceAPI;Integrated Security=True; Trust Server Certificate=True");
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ECommerceAPI;Integrated Security=True; Trust Server Certificate=True");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}
