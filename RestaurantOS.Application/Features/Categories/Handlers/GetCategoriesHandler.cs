using MediatR;
using RestaurantOS.Application.DTOs.Categories;
using RestaurantOS.Application.Features.Categories.Queries;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.Mappings;
namespace RestaurantOS.Application.Features.Categories.Handlers;
public sealed class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<CategoryDto>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync(cancellationToken);
        var categoryDtos = categories
            .Select(category => category.ToDto())
            .ToList();

        return categoryDtos;
    }
}
