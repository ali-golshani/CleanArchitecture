using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Commands.SubmitOrderCommand;

internal sealed class AccessVerifier : AccessVerifierByPermissionRules<Domain.Order>
{
    protected override IPermissionRule<Domain.Order>[] PermissionRules(Domain.Order request)
    {
        return
        [
            new RolesPermissionRule<Domain.Order>(Role.Programmer),
            new CustomerPermissionRule<Domain.Order>(request.CustomerId),
            new BrokerPermissionRule<Domain.Order>(request.BrokerId),
        ];
    }
}
