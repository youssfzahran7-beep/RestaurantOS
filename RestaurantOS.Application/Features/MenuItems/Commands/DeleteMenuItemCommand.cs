using MediatR;
namespace RestaurantOS.Application.Features.MenuItems.Commands;
public sealed record DeleteMenuItemCommand(Guid Id) : IRequest<bool>;
