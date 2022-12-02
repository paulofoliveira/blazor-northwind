using Microsoft.EntityFrameworkCore;

namespace BlazorNorthwind.Api.Infrastructure.Data
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(x => x.CustomerID);
            modelBuilder.Entity<Customer>().Property(x => x.CustomerID).IsRequired();
            modelBuilder.Entity<Customer>().Property(x => x.CompanyName).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Customer>().Property(x => x.ContactName).HasMaxLength(60);
            modelBuilder.Entity<Customer>().Property(x => x.ContactTitle).HasMaxLength(60);
            modelBuilder.Entity<Customer>().Property(x => x.Address).HasMaxLength(120);
            modelBuilder.Entity<Customer>().ToTable("Customers");

            modelBuilder.Entity<Employee>().HasKey(x => x.EmployeeID);
            modelBuilder.Entity<Employee>().Property(x => x.EmployeeID).IsRequired();
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Employee>().Property(x => x.LastName).HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Employee>().ToTable("Employees");

            modelBuilder.Entity<Order>().HasKey(x => x.OrderID);
            modelBuilder.Entity<Order>().HasOne(x => x.Employee).WithMany().HasForeignKey("EmployeeID");
            modelBuilder.Entity<Order>().HasOne(x => x.Customer).WithMany().HasForeignKey("CustomerID");
            modelBuilder.Entity<Order>().Property(x => x.OrderDate);
            modelBuilder.Entity<Order>().OwnsOne(x => x.Ship, q =>
            {
                q.Property(r => r.Date).HasColumnName("ShippedDate");
                q.Property(r => r.Name).HasColumnName("ShipName");
                q.Property(r => r.Address).HasColumnName("ShipAddress");
                q.Property(r => r.City).HasColumnName("ShipCity");
                q.Property(r => r.PostalCode).HasColumnName("ShipPostalCode");
                q.Property(r => r.Country).HasColumnName("ShipCountry");
            });

            modelBuilder.Entity<Order>().ToTable("Orders");

        }
    }
}
