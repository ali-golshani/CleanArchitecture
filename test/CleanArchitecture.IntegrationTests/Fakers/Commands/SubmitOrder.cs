using Bogus;
using CleanArchitecture.Ordering.Commands.Orders.SubmitOrder;

namespace CleanArchitecture.IntegrationTests.Fakers.Commands;

internal sealed class SubmitOrder : Faker<Command>
{
    public SubmitOrder(int orderId)
    {
        RuleFor(x => x.OrderId, orderId);
    }
}
