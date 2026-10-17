using MediatR;

namespace RestaurantOS.Application.Features.Auth.Commands;

public sealed record RevokeTokenCommand(string RefreshToken) : IRequest;
