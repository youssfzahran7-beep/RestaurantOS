using MediatR;
using RestaurantOS.Application.Features.Auth.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Auth.Handlers;

public sealed class RevokeTokenCommandHandler(IAuthService authService) : IRequestHandler<RevokeTokenCommand>
{
    public Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken) =>
        authService.RevokeAsync(request.RefreshToken, cancellationToken);
}
