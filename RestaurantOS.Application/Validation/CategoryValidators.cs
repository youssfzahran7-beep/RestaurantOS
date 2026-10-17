using FluentValidation;
using RestaurantOS.Application.DTOs.Categories;

namespace RestaurantOS.Application.Validation;

public sealed class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
        => RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
}

public sealed class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
        => RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
}
