using MediatR;
using RestaurantOS.Application.DTOs.Employees;
namespace RestaurantOS.Application.Features.Employees.Commands;
public sealed record UpdateEmployeeCommand(Guid Id, UpdateEmployeeRequest Request) : IRequest<bool>;
