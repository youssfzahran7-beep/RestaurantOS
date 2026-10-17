using MediatR;
using RestaurantOS.Application.DTOs.MenuItems;
namespace RestaurantOS.Application.Features.MenuItems.Commands;
public sealed record CreateMenuItemCommand(CreateMenuItemRequest Request) : IRequest<MenuItemDto>;
