using Microsoft.EntityFrameworkCore;
using RestaurantOS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RestaurantOS.Infrastructure.Identity;

namespace RestaurantOS.Infrastructure.Persistence;

public class RestaurantDbContext : IdentityDbContext<ApplicationUser>
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<RestaurantTable> RestaurantTables => Set<RestaurantTable>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);
    }
}
