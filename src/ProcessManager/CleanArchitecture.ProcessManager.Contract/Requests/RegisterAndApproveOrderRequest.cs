using CleanArchitecture.Mediator.Requests;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Requests;

public class RegisterAndApproveOrderRequest :
    RequestBase,
    IOrderRequest,
    IRequest<RegisterAndApproveOrderRequest, Empty>
{
    public override string RequestTitle => "Register and Approve Order";

    public int OrderId { get; init; }
    public int BrokerId { get; init; }
    public int CustomerId { get; init; }
    public int CommodityId { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}
