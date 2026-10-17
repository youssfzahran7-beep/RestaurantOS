using MediatR;
using RestaurantOS.Application.Features.Tables.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Tables.Handlers;

public sealed class DeleteTableHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTableCommand, bool>
{
    public async Task<bool> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        var table = await unitOfWork.RestaurantTables.GetByIdAsync(request.Id, cancellationToken);
        if (table is null) return false;

        unitOfWork.RestaurantTables.Delete(table);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
