using MediatR;
using RestaurantOS.Application.DTOs.MenuItems;
namespace RestaurantOS.Application.Features.MenuItems.Commands;
public sealed record UpdateMenuItemCommand(Guid Id, UpdateMenuItemRequest Request) : IRequest<bool>;
