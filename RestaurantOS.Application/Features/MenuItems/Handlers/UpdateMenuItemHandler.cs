using MediatR;
using RestaurantOS.Application.Features.MenuItems.Commands;
using RestaurantOS.Application.Interfaces;
namespace RestaurantOS.Application.Features.MenuItems.Handlers;
public sealed class UpdateMenuItemHandler : IRequestHandler<UpdateMenuItemCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMenuItemHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        UpdateMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _unitOfWork.MenuItems.GetByIdAsync(request.Id, cancellationToken);
        if (menuItem is null)
        {
            return false;
        }

        var category = await _unitOfWork.Categories.GetByIdAsync(
            request.Request.CategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new KeyNotFoundException("Category was not found.");
        }

        menuItem.UpdateDetails(
            request.Request.Name,
            request.Request.Description,
            request.Request.Price,
            category);
        menuItem.SetAvailability(request.Request.IsAvailable);

        _unitOfWork.MenuItems.Update(menuItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
