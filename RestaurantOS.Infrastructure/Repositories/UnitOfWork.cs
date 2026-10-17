using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Infrastructure.Persistence;

namespace RestaurantOS.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly RestaurantDbContext _context;

    public UnitOfWork(
        RestaurantDbContext context,
        IGenericRepository<Category> categories,
        IMenuRepository menuItems,
        IGenericRepository<RestaurantTable> restaurantTables,
        IOrderRepository orders,
        IGenericRepository<Bill> bills,
        IGenericRepository<InventoryItem> inventoryItems,
        IGenericRepository<Employee> employees)
    {
        _context = context;
        Categories = categories;
        MenuItems = menuItems;
        RestaurantTables = restaurantTables;
        Orders = orders;
        Bills = bills;
        InventoryItems = inventoryItems;
        Employees = employees;
    }

    public IGenericRepository<Category> Categories { get; }
    public IMenuRepository MenuItems { get; }
    public IGenericRepository<RestaurantTable> RestaurantTables { get; }
    public IOrderRepository Orders { get; }
    public IGenericRepository<Bill> Bills { get; }
    public IGenericRepository<InventoryItem> InventoryItems { get; }
    public IGenericRepository<Employee> Employees { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
