using MediatR;
using RestaurantOS.Application.Features.MenuItems.Commands;
using RestaurantOS.Application.Interfaces;
namespace RestaurantOS.Application.Features.MenuItems.Handlers;
public sealed class DeleteMenuItemHandler : IRequestHandler<DeleteMenuItemCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMenuItemHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        DeleteMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _unitOfWork.MenuItems.GetByIdAsync(request.Id, cancellationToken);
        if (menuItem is null)
        {
            return false;
        }

        _unitOfWork.MenuItems.Delete(menuItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
