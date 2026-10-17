using MediatR;
namespace RestaurantOS.Application.Features.Tables.Commands;
public sealed record DeleteTableCommand(Guid Id) : IRequest<bool>;
