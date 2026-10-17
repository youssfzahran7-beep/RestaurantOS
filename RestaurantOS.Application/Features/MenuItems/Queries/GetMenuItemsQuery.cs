using MediatR;
using RestaurantOS.Application.DTOs.Common;
using RestaurantOS.Application.DTOs.MenuItems;
namespace RestaurantOS.Application.Features.MenuItems.Queries;
public sealed record GetMenuItemsQuery(
    string? Search,
    Guid? CategoryId,
    bool IncludeUnavailable = false,
    int Page = 1,
    int PageSize = 20) : IRequest<PagedResult<MenuItemDto>>;
