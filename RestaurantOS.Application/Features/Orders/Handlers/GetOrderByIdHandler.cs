using MediatR;
using RestaurantOS.Application.Features.Orders.Queries;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.DTOs.Orders;

namespace RestaurantOS.Application.Features.Orders.Handlers;

public sealed class GetOrderByIdHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.GetWithItemsAsync(request.Id, cancellationToken);
        if (order is null) return null;

        var items = order.OrderItems.Select(item =>
            new OrderItemDto(item.Id, item.MenuItemId, item.MenuItem.Name, item.Quantity, item.UnitPrice)).ToList();
        return new OrderDto(order.Id, order.RestaurantTableId, order.CreatedAtUtc, items);
    }
}
