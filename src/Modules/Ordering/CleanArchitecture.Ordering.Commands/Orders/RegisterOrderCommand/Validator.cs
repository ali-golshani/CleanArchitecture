using FluentValidation;

namespace CleanArchitecture.Ordering.Commands.Orders.RegisterOrderCommand;

internal sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Price).GreaterThan(100).WithMessage("قیمت سفارش باید بزرگتر از صد باشد");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("مقدار سفارش باید بزرگتر از صفر باشد");
    }
}
