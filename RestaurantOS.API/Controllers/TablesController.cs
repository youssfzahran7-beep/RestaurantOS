using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestaurantOS.Application.DTOs.Tables;
using RestaurantOS.Application.Features.Tables;
using RestaurantOS.Application.Features.Tables.Commands;
using RestaurantOS.Application.Features.Tables.Queries;

namespace RestaurantOS.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Waiter")]
[Route("api/tables")]
public sealed class TablesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await sender.Send(new GetTablesQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var table = await sender.Send(new GetTableByIdQuery(id), cancellationToken);
        return table is null ? NotFound() : Ok(table);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTableRequest request, CancellationToken cancellationToken)
    {
        var table = await sender.Send(new CreateTableCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = table.Id }, table);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTableRequest request, CancellationToken cancellationToken)
        => await sender.Send(new UpdateTableCommand(id, request), cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await sender.Send(new DeleteTableCommand(id), cancellationToken) ? NoContent() : NotFound();
}
