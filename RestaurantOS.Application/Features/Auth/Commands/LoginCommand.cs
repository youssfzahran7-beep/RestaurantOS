using MediatR;
using RestaurantOS.Application.DTOs.Auth;

namespace RestaurantOS.Application.Features.Auth.Commands;

public sealed record LoginCommand(string Email, string Password) : IRequest<AuthTokenResponse?>;
