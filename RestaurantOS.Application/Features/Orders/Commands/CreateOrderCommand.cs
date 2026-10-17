using MediatR;
using RestaurantOS.Application.DTOs.Orders;
namespace RestaurantOS.Application.Features.Orders.Commands;
public sealed record CreateOrderCommand(CreateOrderRequest Request) : IRequest<OrderDto>;
