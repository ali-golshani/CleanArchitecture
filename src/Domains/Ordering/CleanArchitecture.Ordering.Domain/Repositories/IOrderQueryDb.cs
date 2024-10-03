namespace CleanArchitecture.Ordering.Domain.Repositories;

public interface IOrderQueryDb
{
    IQueryable<T> QuerySet<T>() where T : class;
}
