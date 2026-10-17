using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestaurantOS.Application.DTOs.Orders;
using RestaurantOS.Application.Features.Orders;
using RestaurantOS.Application.Features.Orders.Commands;
using RestaurantOS.Application.Features.Orders.Queries;

namespace RestaurantOS.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Waiter,Chef,Cashier")]
[Route("api/orders")]
public sealed class OrdersController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await sender.Send(new GetOrderByIdQuery(id), cancellationToken);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await sender.Send(new CreateOrderCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }
}
