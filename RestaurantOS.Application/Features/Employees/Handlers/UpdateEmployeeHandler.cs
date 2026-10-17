using MediatR;
using RestaurantOS.Application.Features.Employees.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Employees.Handlers;

public sealed class UpdateEmployeeHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEmployeeCommand, bool>
{
    public async Task<bool> Handle(
        UpdateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = await unitOfWork.Employees.GetByIdAsync(request.Id, cancellationToken);
        if (employee is null)
            return false;

        employee.UpdateDetails(
            request.Request.FirstName,
            request.Request.LastName,
            request.Request.Role);

        unitOfWork.Employees.Update(employee);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
