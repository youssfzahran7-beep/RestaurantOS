using MediatR;
using RestaurantOS.Application.Features.Employees.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Employees.Handlers;

public sealed class DeleteEmployeeHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEmployeeCommand, bool>
{
    public async Task<bool> Handle(
        DeleteEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = await unitOfWork.Employees.GetByIdAsync(request.Id, cancellationToken);
        if (employee is null)
            return false;

        unitOfWork.Employees.Delete(employee);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
