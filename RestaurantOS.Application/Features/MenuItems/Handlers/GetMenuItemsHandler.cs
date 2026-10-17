using MediatR;
using RestaurantOS.Application.DTOs.Common;
using RestaurantOS.Application.DTOs.MenuItems;
using RestaurantOS.Application.Features.MenuItems.Queries;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.Mappings;
namespace RestaurantOS.Application.Features.MenuItems.Handlers;
public sealed class GetMenuItemsHandler : IRequestHandler<GetMenuItemsQuery, PagedResult<MenuItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMenuItemsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<MenuItemDto>> Handle(
        GetMenuItemsQuery request,
        CancellationToken cancellationToken)
    {
        var page = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var result = await _unitOfWork.MenuItems.SearchPagedAsync(
            request.Search,
            request.CategoryId,
            request.IncludeUnavailable,
            page,
            pageSize,
            cancellationToken);

        var menuItemDtos = result.Items
            .Select(menuItem => menuItem.ToDto())
            .ToList();

        return new PagedResult<MenuItemDto>(
            menuItemDtos,
            result.Page,
            result.PageSize,
            result.TotalCount);
    }
}
