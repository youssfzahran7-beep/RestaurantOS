using MediatR;
using RestaurantOS.Application.Features.Tables.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Tables.Handlers;

public sealed class UpdateTableHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTableCommand, bool>
{
    public async Task<bool> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await unitOfWork.RestaurantTables.GetByIdAsync(request.Id, cancellationToken);
        if (table is null) return false;

        table.UpdateDetails(request.Request.Number, request.Request.Capacity);
        unitOfWork.RestaurantTables.Update(table);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
