using FluentValidation;
using RestaurantOS.Application.DTOs.Tables;

namespace RestaurantOS.Application.Validation;

public sealed class CreateTableRequestValidator : AbstractValidator<CreateTableRequest>
{
    public CreateTableRequestValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Capacity).GreaterThan(0);
    }
}

public sealed class UpdateTableRequestValidator : AbstractValidator<UpdateTableRequest>
{
    public UpdateTableRequestValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Capacity).GreaterThan(0);
    }
}
