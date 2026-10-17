namespace RestaurantOS.Application.DTOs.Orders;

public sealed record CreateOrderItemRequest(Guid MenuItemId, int Quantity);
public sealed record CreateOrderRequest(Guid RestaurantTableId, IReadOnlyList<CreateOrderItemRequest> Items);
public sealed record OrderItemDto(Guid Id, Guid MenuItemId, string MenuItemName, int Quantity, decimal UnitPrice);
public sealed record OrderDto(Guid Id, Guid RestaurantTableId, DateTime CreatedAtUtc, IReadOnlyList<OrderItemDto> Items);
