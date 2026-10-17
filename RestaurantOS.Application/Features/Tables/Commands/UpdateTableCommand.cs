using MediatR;
using RestaurantOS.Application.DTOs.Tables;
namespace RestaurantOS.Application.Features.Tables.Commands;
public sealed record UpdateTableCommand(Guid Id, UpdateTableRequest Request) : IRequest<bool>;
