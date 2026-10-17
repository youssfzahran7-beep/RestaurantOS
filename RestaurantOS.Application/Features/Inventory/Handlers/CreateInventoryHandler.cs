using MediatR;
using RestaurantOS.Application.DTOs.Inventory;
using RestaurantOS.Application.Features.Inventory.Commands;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Features.Inventory.Handlers;

public sealed class CreateInventoryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInventoryCommand, InventoryItemDto>
{
    public async Task<InventoryItemDto> Handle(
        CreateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        var item = new InventoryItem(
            request.Request.Name,
            request.Request.Quantity,
            request.Request.Unit);

        await unitOfWork.InventoryItems.AddAsync(item, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new InventoryItemDto(item.Id, item.Name, item.Quantity, item.Unit);
    }
}
