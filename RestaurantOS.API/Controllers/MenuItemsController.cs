using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestaurantOS.Application.DTOs.MenuItems;
using RestaurantOS.Application.Features.MenuItems;
using RestaurantOS.Application.Features.MenuItems.Commands;
using RestaurantOS.Application.Features.MenuItems.Queries;

namespace RestaurantOS.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Waiter,Chef")]
[Route("api/menu-items")]
public sealed class MenuItemsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
        => Ok(await sender.Send(
            new GetMenuItemsQuery(search, categoryId, User.IsInRole("Admin"), page, pageSize),
            cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MenuItemDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await sender.Send(new GetMenuItemByIdQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<MenuItemDto>> Create(
        CreateMenuItemRequest request,
        CancellationToken cancellationToken)
    {
        var item = await sender.Send(new CreateMenuItemCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateMenuItemRequest request,
        CancellationToken cancellationToken)
        => await sender.Send(new UpdateMenuItemCommand(id, request), cancellationToken)
            ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await sender.Send(new DeleteMenuItemCommand(id), cancellationToken)
            ? NoContent() : NotFound();
}
