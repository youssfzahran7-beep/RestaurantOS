using MediatR;
using RestaurantOS.Application.DTOs.Employees;
namespace RestaurantOS.Application.Features.Employees.Queries;
public sealed record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;
