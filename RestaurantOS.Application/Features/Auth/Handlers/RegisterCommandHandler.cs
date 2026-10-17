using MediatR;
using RestaurantOS.Application.DTOs.Auth;
using RestaurantOS.Application.Features.Auth.Commands;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Application.Features.Auth.Handlers;

public sealed class RegisterCommandHandler(IAuthService authService)
    : IRequestHandler<RegisterCommand, AuthOperationResult>
{
    public Task<AuthOperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken) =>
        authService.RegisterAsync(request, cancellationToken);
}
