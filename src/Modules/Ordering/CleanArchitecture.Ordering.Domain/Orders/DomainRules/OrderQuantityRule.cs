namespace CleanArchitecture.Ordering.Domain.Orders.DomainRules;

public sealed class OrderQuantityRule(int quantity) : IDomainRule
{
    public int Quantity { get; } = quantity;

    public IEnumerable<Clause> Evaluate()
    {
        return
        [
            new Clause
            (
                Quantity > 0,
                Resources.RuleMessages.OrderQuantityRule,
                (nameof(Quantity), Quantity)
            )
        ];
    }
}
