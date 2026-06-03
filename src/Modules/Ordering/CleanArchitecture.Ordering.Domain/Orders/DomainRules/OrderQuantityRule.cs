namespace CleanArchitecture.Ordering.Domain.Orders.DomainRules;

public sealed class OrderQuantityRule(int quantity) : IDomainRule
{
    public int Quantity { get; } = quantity;

    public IEnumerable<Error> Evaluate()
    {
        if (Quantity <= 0)
        {
            yield return new Error(ErrorType.Validation, Resources.RuleMessages.OrderQuantityRule, (nameof(Quantity), Quantity));
        }
    }
}
