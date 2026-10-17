using MediatR;
using RestaurantOS.Application.DTOs.MenuItems;
using RestaurantOS.Application.Features.MenuItems.Commands;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.Mappings;
using RestaurantOS.Domain.Entities;
namespace RestaurantOS.Application.Features.MenuItems.Handlers;
public sealed class CreateMenuItemHandler : IRequestHandler<CreateMenuItemCommand, MenuItemDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMenuItemHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MenuItemDto> Handle(
        CreateMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(
            request.Request.CategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new KeyNotFoundException("Category was not found.");
        }

        var menuItem = new MenuItem(
            request.Request.Name,
            request.Request.Description,
            request.Request.Price,
            category);

        await _unitOfWork.MenuItems.AddAsync(menuItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return menuItem.ToDto();
    }
}
