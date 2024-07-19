using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Orders.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Orders.Repo.Context
{
    public class OrderManagementDbContext:IdentityDbContext<User>
    {
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderModel>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);


            });
           


            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet <OrderModel> Orders { get; set;}
        public DbSet <OrderItem> OrderItems { get; set; }
        public DbSet <Product> Products { get; set; }
    }
}
