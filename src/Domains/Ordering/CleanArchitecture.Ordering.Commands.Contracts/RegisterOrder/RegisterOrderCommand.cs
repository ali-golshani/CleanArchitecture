﻿using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.RegisterOrder;

public class RegisterOrderCommand :
    CommandBase,
    Mediator.IOrderRequest,
    ICommand<RegisterOrderCommand, Empty>
{
    public override string RequestTitle => "Register Order";

    public int OrderId { get; init; }
    public int BrokerId { get; init; }
    public int CustomerId { get; init; }
    public int CommodityId { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}
