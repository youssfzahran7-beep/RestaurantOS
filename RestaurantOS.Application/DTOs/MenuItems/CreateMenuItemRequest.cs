namespace RestaurantOS.Application.DTOs.MenuItems;

public sealed record CreateMenuItemRequest(
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId);
