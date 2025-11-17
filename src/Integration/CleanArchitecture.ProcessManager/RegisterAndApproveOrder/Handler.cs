using DurableTask.Core;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Handler(TaskHubClient client) : IRequestHandler<Request, Empty>
{
    private readonly TaskHubClient client = client;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        await client.CreateOrchestrationInstanceAsync(typeof(Orchestration), request);
        return Empty.Value;
    }
}
