using Infrastructure.CommoditySystem;

namespace CleanArchitecture.Ordering.Commands.RegisterOrderCommand;

internal sealed class EnrichedCommand : Command
{
    public required Commodity Commodity { get; init; }
}
