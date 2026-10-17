using MediatR;
using RestaurantOS.Application.Features.Orders.Commands;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.DTOs.Orders;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Features.Orders.Handlers;

public sealed class CreateOrderHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (request.Request.Items.Count == 0)
            throw new ArgumentException("An order must include at least one item.");

        var table = await unitOfWork.RestaurantTables.GetByIdAsync(request.Request.RestaurantTableId, cancellationToken)
            ?? throw new KeyNotFoundException("Restaurant table was not found.");
        var order = new Order(table);

        foreach (var itemRequest in request.Request.Items)
        {
            var menuItem = await unitOfWork.MenuItems.GetByIdAsync(itemRequest.MenuItemId, cancellationToken)
                ?? throw new KeyNotFoundException("Menu item was not found.");
            if (!menuItem.IsAvailable)
                throw new InvalidOperationException($"Menu item '{menuItem.Name}' is unavailable.");

            order.AddItem(menuItem, itemRequest.Quantity);
        }

        await unitOfWork.Orders.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var items = order.OrderItems.Select(item =>
            new OrderItemDto(item.Id, item.MenuItemId, item.MenuItem.Name, item.Quantity, item.UnitPrice)).ToList();
        return new OrderDto(order.Id, order.RestaurantTableId, order.CreatedAtUtc, items);
    }
}
