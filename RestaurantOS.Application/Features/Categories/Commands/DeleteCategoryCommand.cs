using MediatR;

namespace RestaurantOS.Application.Features.Categories.Commands;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest<bool>;
