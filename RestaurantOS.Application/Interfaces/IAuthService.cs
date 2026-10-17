using RestaurantOS.Application.DTOs.Auth;
using RestaurantOS.Application.Features.Auth.Commands;

namespace RestaurantOS.Application.Interfaces;

public interface IAuthService
{
    Task<AuthOperationResult> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);
    Task<AuthTokenResponse?> LoginAsync(LoginCommand command, CancellationToken cancellationToken);
    Task<AuthTokenResponse?> RefreshAsync(string refreshToken, CancellationToken cancellationToken);
    Task RevokeAsync(string refreshToken, CancellationToken cancellationToken);
}
