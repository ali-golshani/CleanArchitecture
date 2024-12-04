namespace CleanArchitecture.Ordering.Domain.Repositories;

public interface IOrderingQueryDb
{
    IQueryable<T> QuerySet<T>() where T : class;
}
