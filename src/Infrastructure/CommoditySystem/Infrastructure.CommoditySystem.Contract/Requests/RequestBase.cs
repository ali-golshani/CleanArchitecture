﻿using Framework.Mediator;

namespace Infrastructure.CommoditySystem;

public abstract class RequestBase : Request
{
    private protected RequestBase() { }

    public override bool? ShouldLog => false;
}
