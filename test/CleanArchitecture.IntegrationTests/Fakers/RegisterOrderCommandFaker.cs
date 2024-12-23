using Bogus;
using CleanArchitecture.Ordering.Commands.Orders.RegisterOrderCommand;

namespace CleanArchitecture.IntegrationTests.Fakers;

internal class RegisterOrderCommandFaker : Faker<Command>
{
    public RegisterOrderCommandFaker(int orderId, int customerId, int commodityId)
    {
        RuleFor(x => x.OrderId, orderId);
        RuleFor(x => x.CustomerId, customerId);
        RuleFor(x => x.CommodityId, commodityId);
        RuleFor(x => x.BrokerId, x => x.Random.Number(10, 90));
        RuleFor(x => x.Price, x => x.Random.Decimal(1000, 100_000));
        RuleFor(x => x.Quantity, x => x.Random.Number(100, 1000));
    }
}
