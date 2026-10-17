using Microsoft.EntityFrameworkCore;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Application.DTOs.Common;
using RestaurantOS.Domain.Entities;
using RestaurantOS.Infrastructure.Persistence;

namespace RestaurantOS.Infrastructure.Repositories;

public sealed class MenuRepository : GenericRepository<MenuItem>, IMenuRepository
{
    private readonly RestaurantDbContext _context;

    public MenuRepository(RestaurantDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MenuItem>> GetAvailableByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.MenuItems
            .AsNoTracking()
            .Where(item => item.CategoryId == categoryId && item.IsAvailable)
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MenuItem>> SearchAsync(
        string? searchTerm,
        CancellationToken cancellationToken = default)
    {
        var query = _context.MenuItems.AsNoTracking().Where(item => item.IsAvailable);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(item =>
                item.Name.Contains(term) ||
                (item.Description != null && item.Description.Contains(term)));
        }

        return await query.OrderBy(item => item.Name).ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<MenuItem>> SearchPagedAsync(
        string? searchTerm,
        Guid? categoryId,
        bool includeUnavailable,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _context.MenuItems.AsNoTracking();
        if (!includeUnavailable)
            query = query.Where(item => item.IsAvailable);
        if (categoryId.HasValue)
            query = query.Where(item => item.CategoryId == categoryId.Value);
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(item => item.Name.Contains(term) ||
                (item.Description != null && item.Description.Contains(term)));
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.OrderBy(item => item.Name)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<MenuItem>(items, page, pageSize, totalCount);
    }
}
