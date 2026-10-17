using MediatR;
using RestaurantOS.Application.DTOs.MenuItems;
namespace RestaurantOS.Application.Features.MenuItems.Queries;
public sealed record GetMenuItemByIdQuery(Guid Id) : IRequest<MenuItemDto?>;
