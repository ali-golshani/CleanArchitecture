using CleanArchitecture.Mediator.Middlewares.Transformers;
using Framework.Results;
using Framework.Results.Extensions;
using Infrastructure.CommoditySystem;

namespace CleanArchitecture.Ordering.Commands.RegisterOrderCommand;

internal sealed class RequestEnricher : EnrichingTransformer<Command>
{
    private readonly ICommoditySystem commoditySystem;

    public RequestEnricher(ICommoditySystem commoditySystem)
    {
        this.commoditySystem = commoditySystem;
    }

    public override int Order { get; } = 1;

    protected override async ValueTask<Result<Command>> Enrich(Command value, CancellationToken cancellationToken)
    {
        var commodityResult = await GetCommodity(value.CommodityId, cancellationToken);

        if (commodityResult.IsFailure)
        {
            return commodityResult.Errors;
        }

        var commodity = commodityResult.Value!;

        return new EnrichedCommand
        {
            OrderId = value.OrderId,
            BrokerId = value.BrokerId,
            CustomerId = value.CustomerId,
            CommodityId = value.CommodityId,
            Price = value.Price,
            Quantity = value.Quantity,
            Commodity = commodity,
        };
    }

    private async Task<Result<Commodity>> GetCommodity(int commodityId, CancellationToken cancellationToken)
    {
        var request = new CommodityRequest
        {
            CommodityId = commodityId
        };

        return await
            commoditySystem
            .Handle(request, cancellationToken)
            .NotFoundIfNull(PersianDictionary.CommodityDictionary.Commodity, commodityId);
    }
}
