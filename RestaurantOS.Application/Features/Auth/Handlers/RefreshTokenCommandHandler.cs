using MediatR;
using RestaurantOS.Application.DTOs.Auth;
using RestaurantOS.Application.Features.Auth.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Auth.Handlers;

public sealed class RefreshTokenCommandHandler(IAuthService authService)
    : IRequestHandler<RefreshTokenCommand, AuthTokenResponse?>
{
    public Task<AuthTokenResponse?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken) =>
        authService.RefreshAsync(request.RefreshToken, cancellationToken);
}
