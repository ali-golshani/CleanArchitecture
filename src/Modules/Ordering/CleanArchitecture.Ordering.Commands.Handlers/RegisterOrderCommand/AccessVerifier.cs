using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Commands.RegisterOrderCommand;

internal sealed class AccessVerifier : AccessVerifierByPermissionRules<Command>
{
    protected override IPermissionRule<Command>[] PermissionRules(Command request)
    {
        return
        [
            new RolesPermissionRule<Command>(Role.Programmer),
            new CustomerPermissionRule<Command>(request.CustomerId),
            new BrokerPermissionRule<Command>(request.BrokerId),
        ];
    }
}
