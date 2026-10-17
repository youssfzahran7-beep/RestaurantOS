using MediatR;
using RestaurantOS.Application.DTOs.Categories;

namespace RestaurantOS.Application.Features.Categories.Queries;

public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;
