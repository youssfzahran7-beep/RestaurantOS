using MediatR;
using RestaurantOS.Application.DTOs.Categories;

namespace RestaurantOS.Application.Features.Categories.Commands;

public sealed record UpdateCategoryCommand(Guid Id, UpdateCategoryRequest Request) : IRequest<bool>;
