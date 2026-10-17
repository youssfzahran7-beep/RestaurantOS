using MediatR;
using RestaurantOS.Application.DTOs.Categories;

namespace RestaurantOS.Application.Features.Categories.Queries;

public sealed record GetCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>;
