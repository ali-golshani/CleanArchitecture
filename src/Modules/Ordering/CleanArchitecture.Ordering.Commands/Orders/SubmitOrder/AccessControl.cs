using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Ordering.Domain.Orders;

namespace CleanArchitecture.Ordering.Commands.Orders.SubmitOrder;

internal sealed class AccessControl : AccessControlByPermissionRules<Order>
{
    protected override IPermissionRule<Order>[] PermissionRules(Order content)
    {
        return
        [
            new RolesPermissionRule<Order>(Role.Programmer),
            new CustomerPermissionRule<Order>(content.CustomerId),
            new BrokerPermissionRule<Order>(content.BrokerId),
        ];
    }
}
