using MediatR;
using RestaurantOS.Application.Features.Tables.Queries;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.DTOs.Tables;

namespace RestaurantOS.Application.Features.Tables.Handlers;

public sealed class GetTablesHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTablesQuery, IReadOnlyList<RestaurantTableDto>>
{
    public async Task<IReadOnlyList<RestaurantTableDto>> Handle(GetTablesQuery request, CancellationToken cancellationToken)
    {
        var tables = await unitOfWork.RestaurantTables.GetAllAsync(cancellationToken);
        return tables.Select(table => new RestaurantTableDto(table.Id, table.Number, table.Capacity)).ToList();
    }
}
