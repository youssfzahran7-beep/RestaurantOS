using Microsoft.EntityFrameworkCore;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Infrastructure.Persistence;

namespace RestaurantOS.Infrastructure.Repositories;

public sealed class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly RestaurantDbContext _context;

    public OrderRepository(RestaurantDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Order?> GetWithItemsAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        return _context.Orders
            .AsNoTracking()
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.MenuItem)
            .SingleOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }
}
