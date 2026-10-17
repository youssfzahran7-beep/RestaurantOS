using MediatR;
using RestaurantOS.Application.Features.Tables.Queries;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.DTOs.Tables;

namespace RestaurantOS.Application.Features.Tables.Handlers;

public sealed class GetTableByIdHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTableByIdQuery, RestaurantTableDto?>
{
    public async Task<RestaurantTableDto?> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        var table = await unitOfWork.RestaurantTables.GetByIdAsync(request.Id, cancellationToken);
        return table is null ? null : new RestaurantTableDto(table.Id, table.Number, table.Capacity);
    }
}
