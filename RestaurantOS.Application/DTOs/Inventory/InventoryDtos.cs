namespace RestaurantOS.Application.DTOs.Inventory;
public sealed record InventoryItemDto(Guid Id, string Name, decimal Quantity, string Unit);
public sealed record CreateInventoryItemRequest(string Name, decimal Quantity, string Unit);
