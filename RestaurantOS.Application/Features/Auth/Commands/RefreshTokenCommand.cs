using MediatR;
using RestaurantOS.Application.DTOs.Auth;

namespace RestaurantOS.Application.Features.Auth.Commands;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<AuthTokenResponse?>;
