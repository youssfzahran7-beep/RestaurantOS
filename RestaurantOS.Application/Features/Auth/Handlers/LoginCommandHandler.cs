using MediatR;
using RestaurantOS.Application.DTOs.Auth;
using RestaurantOS.Application.Features.Auth.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Auth.Handlers;

public sealed class LoginCommandHandler(IAuthService authService)
    : IRequestHandler<LoginCommand, AuthTokenResponse?>
{
    public Task<AuthTokenResponse?> Handle(LoginCommand request, CancellationToken cancellationToken) =>
        authService.LoginAsync(request, cancellationToken);
}
