using MediatR;
using RestaurantOS.Application.DTOs.Employees;
using RestaurantOS.Application.Features.Employees.Commands;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Features.Employees.Handlers;

public sealed class CreateEmployeeHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = new Employee(
            request.Request.FirstName,
            request.Request.LastName,
            request.Request.Role);

        await unitOfWork.Employees.AddAsync(employee, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new EmployeeDto(employee.Id, employee.FirstName, employee.LastName, employee.Role);
    }
}
