using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOS.Application.DTOs.Inventory;
using RestaurantOS.Application.Features.Inventory.Commands;
using RestaurantOS.Application.Features.Inventory.Queries;

namespace RestaurantOS.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Chef")]
[Route("api/inventory")]
public sealed class InventoryController : ControllerBase
{
    private readonly ISender _sender;

    public InventoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var items = await _sender.Send(new GetInventoryQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateInventoryItemRequest request,
        CancellationToken cancellationToken)
    {
        var item = await _sender.Send(new CreateInventoryCommand(request), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }
}
