using RestaurantOS.Application.DTOs.Categories;
using RestaurantOS.Application.DTOs.MenuItems;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Mappings;

public static class DomainMappingExtensions
{
    public static CategoryDto ToDto(this Category category)
        => new(category.Id, category.Name);

    public static MenuItemDto ToDto(this MenuItem menuItem)
        => new(menuItem.Id, menuItem.Name, menuItem.Description,
            menuItem.Price, menuItem.CategoryId, menuItem.IsAvailable);
}
