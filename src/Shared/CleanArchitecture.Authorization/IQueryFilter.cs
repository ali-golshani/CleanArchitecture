namespace CleanArchitecture.Authorization;

public interface IQueryFilter<TQuery>
{
    TQuery Filter(Actor? actor, TQuery query);
}