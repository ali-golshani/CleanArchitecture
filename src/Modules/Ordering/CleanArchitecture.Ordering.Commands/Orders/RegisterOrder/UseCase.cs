using CleanArchitecture.Actors;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

public sealed class UseCase(ActorPreservingScopeFactory scopeFactory) : UseCase<Command, Empty>(scopeFactory);
