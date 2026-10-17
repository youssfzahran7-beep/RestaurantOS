using FluentValidation;
using RestaurantOS.Application.DTOs.Inventory;

namespace RestaurantOS.Application.Validation;

public sealed class CreateInventoryItemRequestValidator : AbstractValidator<CreateInventoryItemRequest>
{
    public CreateInventoryItemRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(150);
        RuleFor(request => request.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(request => request.Unit).NotEmpty().MaximumLength(30);
    }
}
