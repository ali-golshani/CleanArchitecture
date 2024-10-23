using FluentValidation;

namespace CleanArchitecture.Ordering.Commands.RegisterOrder;

internal class Validator : AbstractValidator<RegisterOrderCommand>
{
    public Validator()
    {
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("قیمت سفارش باید بزرگتر از صفر باشد");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("مقدار سفارش باید بزرگتر از صفر باشد");
    }
}
