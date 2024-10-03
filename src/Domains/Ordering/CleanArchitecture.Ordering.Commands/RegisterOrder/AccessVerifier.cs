using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Commands.RegisterOrder;

internal class AccessVerifier : AccessVerifierByPermissionRules<RegisterOrderCommand>
{
    protected override IPermissionRule<RegisterOrderCommand>[] PermissionRules(RegisterOrderCommand request)
    {
        return
        [
            new RolesPermissionRule<RegisterOrderCommand>(Role.Programmer),
            new CustomerPermissionRule<RegisterOrderCommand>(request.CustomerId),
            new BrokerPermissionRule<RegisterOrderCommand>(request.BrokerId),
        ];
    }
}
