using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Commands.SubmitOrderCommand;

internal sealed class AccessControl : AccessControlByPermissionRules<Domain.Order>
{
    protected override IPermissionRule<Domain.Order>[] PermissionRules(Domain.Order content)
    {
        return
        [
            new RolesPermissionRule<Domain.Order>(Role.Programmer),
            new CustomerPermissionRule<Domain.Order>(content.CustomerId),
            new BrokerPermissionRule<Domain.Order>(content.BrokerId),
        ];
    }
}
