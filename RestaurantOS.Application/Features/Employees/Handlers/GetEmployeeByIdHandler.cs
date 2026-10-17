using MediatR;
using RestaurantOS.Application.DTOs.Employees;
using RestaurantOS.Application.Features.Employees.Queries;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Employees.Handlers;

public sealed class GetEmployeeByIdHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    public async Task<EmployeeDto?> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await unitOfWork.Employees.GetByIdAsync(request.Id, cancellationToken);
        return employee is null
            ? null
            : new EmployeeDto(employee.Id, employee.FirstName, employee.LastName, employee.Role);
    }
}
