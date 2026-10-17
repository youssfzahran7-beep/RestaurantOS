using MediatR;
using RestaurantOS.Application.DTOs.Employees;
using RestaurantOS.Application.Features.Employees.Queries;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Employees.Handlers;

public sealed class GetEmployeesHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    public async Task<IReadOnlyList<EmployeeDto>> Handle(
        GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await unitOfWork.Employees.GetAllAsync(cancellationToken);

        return employees
            .Select(employee => new EmployeeDto(employee.Id, employee.FirstName, employee.LastName, employee.Role))
            .ToList();
    }
}
