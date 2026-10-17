using MediatR;
using RestaurantOS.Application.DTOs.Employees;
namespace RestaurantOS.Application.Features.Employees.Commands;
public sealed record CreateEmployeeCommand(CreateEmployeeRequest Request) : IRequest<EmployeeDto>;
