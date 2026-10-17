namespace RestaurantOS.Application.DTOs.MenuItems;

public sealed record MenuItemDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId,
    bool IsAvailable);
