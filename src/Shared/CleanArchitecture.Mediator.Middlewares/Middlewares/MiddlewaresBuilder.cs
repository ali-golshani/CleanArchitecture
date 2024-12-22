using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class MiddlewaresBuilder<TRequest, TResponse>
{
    private readonly List<Item> middlewares = [];

    public IMiddleware<TRequest, TResponse>[] Build(IServiceProvider serviceProvider)
    {
        return middlewares.SelectMany(x => x.Middlewares(serviceProvider)).ToArray();
    }

    public MiddlewaresBuilder<TRequest, TResponse> AddMiddleware<TMiddleware>()
        where TMiddleware : IMiddleware<TRequest, TResponse>
    {
        middlewares.Add(new Item(typeof(TMiddleware), false));
        return this;
    }

    public MiddlewaresBuilder<TRequest, TResponse> AddMiddlewares<TMiddleware>()
        where TMiddleware : IMiddleware<TRequest, TResponse>
    {
        middlewares.Add(new Item(typeof(TMiddleware), true));
        return this;
    }

    private sealed class Item(Type type, bool isCollection)
    {
        private readonly Type type = type;
        private readonly bool isCollection = isCollection;

        public IMiddleware<TRequest, TResponse>[] Middlewares(IServiceProvider serviceProvider)
        {
            return GetMiddlewares(serviceProvider).ToArray();
        }

        private IEnumerable<IMiddleware<TRequest, TResponse>> GetMiddlewares(IServiceProvider serviceProvider)
        {
            if (isCollection)
            {
                var enumerableType = typeof(IEnumerable<>).MakeGenericType(type);
                var middlewares = serviceProvider.GetService(enumerableType) as IEnumerable;
                if (middlewares != null)
                {
                    foreach (var item in middlewares)
                    {
                        var middleware = item as IMiddleware<TRequest, TResponse>;
                        yield return middleware!;
                    }
                }
            }
            else
            {
                var middleware = serviceProvider.GetRequiredService(type) as IMiddleware<TRequest, TResponse>;
                yield return middleware!;
            }
        }
    }
}
