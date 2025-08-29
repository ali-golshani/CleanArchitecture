using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using MassTransit;
using GetOrder = CleanArchitecture.Ordering.Queries.Orders.GetOrder;

namespace CleanArchitecture.Ordering.Application.MassTransit.Consumers;

public class GetOrderQueryConsumer(ICommandService commandService, IQueryService queryService) :
    ConsumerBase(commandService, queryService),
    IConsumer<GetOrder.Query>
{
    public async Task Consume(ConsumeContext<GetOrder.Query> context)
    {
        var result = await Handle(context.Message, context.CancellationToken);
        await context.RespondAsync(result);
    }
}
