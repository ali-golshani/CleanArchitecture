namespace CleanArchitecture.Ordering.Domain.Orders.DomainRules;

public sealed class OrderPriceRule(decimal price) : IDomainRule
{
    public decimal Price { get; } = price;

    public IEnumerable<Error> Evaluate()
    {
        if (Price <= 0)
        {
            yield return new(ErrorType.Validation, Resources.RuleMessages.OrderPriceRule, (nameof(Price), Price));
        }
    }
}
