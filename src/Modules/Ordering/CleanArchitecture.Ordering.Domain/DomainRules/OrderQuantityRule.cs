﻿namespace CleanArchitecture.Ordering.Domain.DomainRules;

public class OrderQuantityRule(int quantity) : IDomainRule
{
    public int Quantity { get; } = quantity;

    public IEnumerable<Clause> Evaluate()
    {
        return
        [
            new Clause
            (
                Quantity > 0,
                "مقدار سفارش باید بزرگتر از صفر باشد",
                (nameof(Quantity), Quantity)
            )
        ];
    }
}