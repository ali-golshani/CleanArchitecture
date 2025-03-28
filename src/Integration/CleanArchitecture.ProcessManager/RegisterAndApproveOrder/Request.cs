﻿using CleanArchitecture.Mediator;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public class Request :
    RequestBase,
    IOrderRequest,
    IRequest<Request, Empty>
{
    public override string RequestTitle => "Register and Approve Order";

    public int OrderId { get; init; }
    public int BrokerId { get; init; }
    public int CustomerId { get; init; }
    public int CommodityId { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}
