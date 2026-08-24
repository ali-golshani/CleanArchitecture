using FluentValidation;

namespace CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

internal sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        // This use case requires a minimum price of 100; the domain only requires a positive price.
        RuleFor(x => x.Price).GreaterThan(100).WithMessage(Resources.ValidationMessages.OrderPriceRule);
        
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(Resources.ValidationMessages.OrderQuantityRule);
    }
}
