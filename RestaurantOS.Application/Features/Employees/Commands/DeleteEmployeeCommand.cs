using MediatR;
namespace RestaurantOS.Application.Features.Employees.Commands;
public sealed record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;
