using FluentValidation;
using RestaurantOS.Application.Features.Categories.Commands;
using RestaurantOS.Application.Features.Employees.Commands;
using RestaurantOS.Application.Features.Inventory.Commands;
using RestaurantOS.Application.Features.MenuItems.Commands;
using RestaurantOS.Application.Features.MenuItems.Queries;
using RestaurantOS.Application.Features.Orders.Commands;
using RestaurantOS.Application.Features.Tables.Commands;

namespace RestaurantOS.Application.Validation;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new CreateCategoryRequestValidator());
}

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new UpdateCategoryRequestValidator());
}

public sealed class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
{
    public CreateMenuItemCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new CreateMenuItemRequestValidator());
}

public sealed class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
{
    public UpdateMenuItemCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new UpdateMenuItemRequestValidator());
}

public sealed class GetMenuItemsQueryValidator : AbstractValidator<GetMenuItemsQuery>
{
    public GetMenuItemsQueryValidator()
    {
        RuleFor(query => query.Page).InclusiveBetween(1, 1_000_000);
        RuleFor(query => query.PageSize).InclusiveBetween(1, 100);
        RuleFor(query => query.Search).MaximumLength(100);
    }
}

public sealed class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new CreateTableRequestValidator());
}

public sealed class UpdateTableCommandValidator : AbstractValidator<UpdateTableCommand>
{
    public UpdateTableCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new UpdateTableRequestValidator());
}

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new CreateOrderRequestValidator());
}

public sealed class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new CreateInventoryItemRequestValidator());
}

public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new CreateEmployeeRequestValidator());
}

public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator() =>
        RuleFor(command => command.Request).SetValidator(new UpdateEmployeeRequestValidator());
}
