using MediatR;
using RestaurantOS.Application.DTOs.Tables;
namespace RestaurantOS.Application.Features.Tables.Queries;
public sealed record GetTablesQuery : IRequest<IReadOnlyList<RestaurantTableDto>>;
