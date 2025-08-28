namespace CleanArchitecture.Ordering.Domain.Orders.DomainRules;

public sealed class OrderPriceRule(decimal price) : IDomainRule
{
    public decimal Price { get; } = price;

    public IEnumerable<Clause> Evaluate()
    {
        return
        [
            new Clause
            (
                Price > 0,
                Resources.RuleMessages.OrderPriceRule,
                (nameof(Price), Price)
            )
        ];
    }
}
