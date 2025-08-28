using FluentValidation;

namespace CleanArchitecture.Ordering.Commands.Orders.RegisterOrderCommand;

internal sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Price).GreaterThan(100).WithMessage(Resources.ValidationMessages.OrderPriceRule);
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(Resources.ValidationMessages.OrderQuantityRule);
    }
}
