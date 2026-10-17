using MediatR;
using RestaurantOS.Application.DTOs.Tables;
namespace RestaurantOS.Application.Features.Tables.Queries;
public sealed record GetTableByIdQuery(Guid Id) : IRequest<RestaurantTableDto?>;
