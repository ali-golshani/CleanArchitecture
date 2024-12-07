namespace CleanArchitecture.Authorization;

public interface IDataFilter<TData>
{
    TData Filter(Actor? actor, TData data);
}