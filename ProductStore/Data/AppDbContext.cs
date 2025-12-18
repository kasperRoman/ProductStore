using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductStore.Models.Product;

namespace ProductStore.Data
{
    
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductStore.Models.Order.Order> Orders { get; set; }
        public DbSet<ProductStore.Models.Order.OrderItem> OrderItems { get; set; }
    }
}