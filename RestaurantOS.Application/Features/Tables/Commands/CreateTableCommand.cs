using MediatR;
using RestaurantOS.Application.DTOs.Tables;
namespace RestaurantOS.Application.Features.Tables.Commands;
public sealed record CreateTableCommand(CreateTableRequest Request) : IRequest<RestaurantTableDto>;
