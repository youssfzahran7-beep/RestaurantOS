using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetWithItemsAsync(
        Guid orderId,
        CancellationToken cancellationToken = default);
}
