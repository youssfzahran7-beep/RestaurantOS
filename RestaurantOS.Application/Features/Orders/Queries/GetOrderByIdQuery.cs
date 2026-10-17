using MediatR;
using RestaurantOS.Application.DTOs.Orders;
namespace RestaurantOS.Application.Features.Orders.Queries;
public sealed record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
