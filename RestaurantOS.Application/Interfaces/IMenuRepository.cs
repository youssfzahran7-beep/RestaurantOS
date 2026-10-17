using RestaurantOS.Domain.Entities;
using RestaurantOS.Application.DTOs.Common;

namespace RestaurantOS.Application.Interfaces;

public interface IMenuRepository : IGenericRepository<MenuItem>
{
    Task<IReadOnlyList<MenuItem>> GetAvailableByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MenuItem>> SearchAsync(
        string? searchTerm,
        CancellationToken cancellationToken = default);

    Task<PagedResult<MenuItem>> SearchPagedAsync(
        string? searchTerm,
        Guid? categoryId,
        bool includeUnavailable,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}
