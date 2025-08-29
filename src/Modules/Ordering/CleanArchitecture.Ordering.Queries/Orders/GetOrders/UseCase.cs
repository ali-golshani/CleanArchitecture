using CleanArchitecture.Actors;

namespace CleanArchitecture.Ordering.Queries.Orders.GetOrders;

public sealed class UseCase(ActorPreservingScopeFactory scopeFactory) : UseCase<Query, Framework.Queries.PaginatedItems<Models.Order>>(scopeFactory);
