using MediatR;
using RestaurantOS.Application.DTOs.Categories;
using RestaurantOS.Application.Features.Categories.Commands;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.Mappings;
using RestaurantOS.Domain.Entities;
namespace RestaurantOS.Application.Features.Categories.Handlers;
public sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category(request.Request.Name);

        await _unitOfWork.Categories.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.ToDto();
    }
}
