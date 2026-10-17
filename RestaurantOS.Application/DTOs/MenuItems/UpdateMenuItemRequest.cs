namespace RestaurantOS.Application.DTOs.MenuItems;

public sealed record UpdateMenuItemRequest(
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId,
    bool IsAvailable);
