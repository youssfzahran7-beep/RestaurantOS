using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOS.Application.DTOs.Employees;
using RestaurantOS.Application.Features.Employees;
using RestaurantOS.Application.Features.Employees.Commands;
using RestaurantOS.Application.Features.Employees.Queries;

namespace RestaurantOS.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/employees")]
public sealed class EmployeesController : ControllerBase
{
    private readonly ISender _sender;

    public EmployeesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var employees = await _sender.Send(new GetEmployeesQuery(), cancellationToken);
        return Ok(employees);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var employee = await _sender.Send(new GetEmployeeByIdQuery(id), cancellationToken);
        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var employee = await _sender.Send(new CreateEmployeeCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var wasUpdated = await _sender.Send(
            new UpdateEmployeeCommand(id, request),
            cancellationToken);

        if (!wasUpdated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var wasDeleted = await _sender.Send(new DeleteEmployeeCommand(id), cancellationToken);
        if (!wasDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
