using CleanArchitecture.Actors;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.SampleEmpty;

public interface IUseCase : IUseCase<Command, Empty>;
internal sealed class UseCase(ActorPreservingScopeFactory scopeFactory) : UseCase<Command, Empty>(scopeFactory), IUseCase;
