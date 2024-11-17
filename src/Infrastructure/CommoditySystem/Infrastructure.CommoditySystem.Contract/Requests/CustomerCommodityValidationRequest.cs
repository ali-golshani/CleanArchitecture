using Framework.Mediator.Requests;

namespace Infrastructure.CommoditySystem;

public class CustomerCommodityValidationRequest : RequestBase, IRequest<CustomerCommodityValidationRequest, bool>
{
    public override string RequestTitle => "اعتبار سنجی ارتباط مشتری و کالا";

    public required int CustomerId { get; init; }
    public required int CommodityId { get; init; }
}
