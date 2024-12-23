namespace CleanArchitecture.Ordering.Domain.Orders.DomainRules;

public class OrderPriceRule(decimal price) : IDomainRule
{
    public decimal Price { get; } = price;

    public IEnumerable<Clause> Evaluate()
    {
        return
        [
            new Clause
            (
                Price > 0,
                "قیمت سفارش باید بزرگتر از صفر باشد",
                (nameof(Price), Price)
            )
        ];
    }
}
