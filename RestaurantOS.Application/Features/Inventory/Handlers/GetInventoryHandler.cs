using MediatR;
using RestaurantOS.Application.DTOs.Inventory;
using RestaurantOS.Application.Features.Inventory.Queries;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Inventory.Handlers;

public sealed class GetInventoryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInventoryQuery, IReadOnlyList<InventoryItemDto>>
{
    public async Task<IReadOnlyList<InventoryItemDto>> Handle(
        GetInventoryQuery request,
        CancellationToken cancellationToken)
    {
        var items = await unitOfWork.InventoryItems.GetAllAsync(cancellationToken);

        return items
            .Select(item => new InventoryItemDto(item.Id, item.Name, item.Quantity, item.Unit))
            .ToList();
    }
}
