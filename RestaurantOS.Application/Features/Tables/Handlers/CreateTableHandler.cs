using MediatR;
using RestaurantOS.Application.Features.Tables.Commands;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.DTOs.Tables;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Features.Tables.Handlers;

public sealed class CreateTableHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTableCommand, RestaurantTableDto>
{
    public async Task<RestaurantTableDto> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        var table = new RestaurantTable(request.Request.Number, request.Request.Capacity);
        await unitOfWork.RestaurantTables.AddAsync(table, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new RestaurantTableDto(table.Id, table.Number, table.Capacity);
    }
}
