using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Commands.RegisterOrderCommand;

internal sealed class AccessControl : AccessControlByPermissionRules<Command>
{
    protected override IPermissionRule<Command>[] PermissionRules(Command content)
    {
        return
        [
            new RolesPermissionRule<Command>(Role.Programmer),
            new CustomerPermissionRule<Command>(content.CustomerId),
            new BrokerPermissionRule<Command>(content.BrokerId),
        ];
    }
}
