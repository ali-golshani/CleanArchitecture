using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using CleanArchitecture.Ordering.Queries.Orders.OrderQuery;
using MassTransit;

namespace CleanArchitecture.Ordering.Application.MassTransit.Consumers;

public class OrderQueryConsumer(ICommandService commandService, IQueryService queryService) :
    ConsumerBase(commandService, queryService),
    IConsumer<Query>
{
    public async Task Consume(ConsumeContext<Query> context)
    {
        var result = await Handle(context.Message, context.CancellationToken);
        await context.RespondAsync(result);
    }
}
