using MediatR; using RestaurantOS.Application.Features.Categories.Queries; using RestaurantOS.Application.Interfaces; using RestaurantOS.Application.Mappings;
namespace RestaurantOS.Application.Features.Categories.Handlers;
public sealed class GetCategoryByIdHandler(IUnitOfWork uow) : IRequestHandler<GetCategoryByIdQuery,DTOs.Categories.CategoryDto?>
{ public async Task<DTOs.Categories.CategoryDto?> Handle(GetCategoryByIdQuery r,CancellationToken c)=>(await uow.Categories.GetByIdAsync(r.Id,c))?.ToDto(); }
