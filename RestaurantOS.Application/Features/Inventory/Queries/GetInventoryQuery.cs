using MediatR;
using RestaurantOS.Application.DTOs.Inventory;
namespace RestaurantOS.Application.Features.Inventory.Queries;
public sealed record GetInventoryQuery : IRequest<IReadOnlyList<InventoryItemDto>>;
