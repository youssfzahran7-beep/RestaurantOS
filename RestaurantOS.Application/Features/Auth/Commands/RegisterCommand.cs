using MediatR;
using RestaurantOS.Application.DTOs.Auth;

namespace RestaurantOS.Application.Features.Auth.Commands;

public sealed record RegisterCommand(string Email, string Password, string FirstName, string LastName)
    : IRequest<AuthOperationResult>;
