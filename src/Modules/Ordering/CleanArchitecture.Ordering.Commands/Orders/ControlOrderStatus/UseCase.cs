using CleanArchitecture.Actors;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Orders.ControlOrderStatus;

public sealed class UseCase(ActorPreservingScopeFactory scopeFactory) : UseCase<Command, Empty>(scopeFactory);
