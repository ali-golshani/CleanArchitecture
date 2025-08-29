using CleanArchitecture.Actors;

namespace CleanArchitecture.Ordering.Queries.Orders.GetOrder;

public sealed class UseCase(ActorPreservingScopeFactory scopeFactory) : UseCase<Query, Models.Order?>(scopeFactory);
