using MediatR;
using RestaurantOS.Application.DTOs.Inventory;
namespace RestaurantOS.Application.Features.Inventory.Commands;
public sealed record CreateInventoryCommand(CreateInventoryItemRequest Request) : IRequest<InventoryItemDto>;
