using MediatR; using RestaurantOS.Application.Features.MenuItems.Queries; using RestaurantOS.Application.Interfaces; using RestaurantOS.Application.Mappings;
namespace RestaurantOS.Application.Features.MenuItems.Handlers;
public sealed class GetMenuItemByIdHandler(IUnitOfWork uow) : IRequestHandler<GetMenuItemByIdQuery,DTOs.MenuItems.MenuItemDto?>
{ public async Task<DTOs.MenuItems.MenuItemDto?> Handle(GetMenuItemByIdQuery r,CancellationToken c)=>(await uow.MenuItems.GetByIdAsync(r.Id,c))?.ToDto(); }
