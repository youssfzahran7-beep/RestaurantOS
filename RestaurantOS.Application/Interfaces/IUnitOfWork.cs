using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Category> Categories { get; }
    IMenuRepository MenuItems { get; }
    IGenericRepository<RestaurantTable> RestaurantTables { get; }
    IOrderRepository Orders { get; }
    IGenericRepository<Bill> Bills { get; }
    IGenericRepository<InventoryItem> InventoryItems { get; }
    IGenericRepository<Employee> Employees { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
