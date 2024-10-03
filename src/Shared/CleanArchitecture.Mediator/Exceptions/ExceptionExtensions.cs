using Framework.DomainRules;

namespace CleanArchitecture.Mediator;

public static class ExceptionExtensions
{
    public static Error ToError(this Clause clause)
    {
        var sources = clause.Sources.Select(x => new ErrorSource(x.Name, x.Value)).ToArray();
        return new Error(ErrorType.Validation, clause.Statement, sources);
    }
}
