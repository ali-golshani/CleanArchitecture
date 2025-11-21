using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

internal sealed class AccessControl : AccessControlByPermissionRules<Command>
{
    protected override IPermissionRule<Command>[] PermissionRules(Command content)
    {
        return
        [
            new RolesPermissionRule<Command>(Role.Programmer),
            new RolesPermissionRule<Command>(Role.InternalService),
            new CustomerPermissionRule<Command>(content.CustomerId),
            new BrokerPermissionRule<Command>(content.BrokerId),
        ];
    }
}
