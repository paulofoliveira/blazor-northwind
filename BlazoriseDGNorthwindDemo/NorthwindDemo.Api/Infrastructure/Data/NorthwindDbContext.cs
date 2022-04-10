using Microsoft.EntityFrameworkCore;

namespace NorthwindDemo.Api.Infrastructure.Data
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(x => x.CustomerId);

            modelBuilder.Entity<Customer>().Property(x => x.CompanyName).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Customer>().Property(x => x.ContactName).HasMaxLength(60);
            modelBuilder.Entity<Customer>().Property(x => x.ContactTitle).HasMaxLength(60);
            modelBuilder.Entity<Customer>().Property(x => x.Address).HasMaxLength(120);
        }
    }
}
