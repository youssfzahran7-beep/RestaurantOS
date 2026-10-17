using MediatR;
using RestaurantOS.Application.DTOs.Categories;

namespace RestaurantOS.Application.Features.Categories.Commands;

public sealed record CreateCategoryCommand(CreateCategoryRequest Request) : IRequest<CategoryDto>;
