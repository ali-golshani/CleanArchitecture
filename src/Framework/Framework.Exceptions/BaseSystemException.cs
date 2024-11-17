namespace Framework.Exceptions;

public abstract class BaseSystemException : Exception
{
    public const string Question = "?";

    public abstract bool ShouldLog { get; }

    public string CorrelationId { get; } = SmallGuid.GetUniqueKey();

    private protected BaseSystemException() { }
    private protected BaseSystemException(string message) : base(message) { }
    private protected BaseSystemException(Exception innerException) : base(innerException.Message, innerException) { }
    private protected BaseSystemException(string message, Exception innerException) : base(message, innerException) { }

    public virtual UserFriendlyException ToUserFriendlyException(bool isRegistered)
    {
        return new UserFriendlyException(Message, CorrelationId, isRegistered);
    }

    public virtual bool IsFatal => false;
    public virtual IReadOnlyCollection<string> Messages => [Message];

    public virtual string? Properties()
    {
        try
        {
            return MultiLines(GetProperties().Select(x => $"({x.Name} , {x.Value})"));
        }
        catch
        {
            return null;
        }
    }

    protected virtual IEnumerable<(string Name, string Value)> GetProperties()
    {
        yield return new(nameof(CorrelationId), CorrelationId);

        var properties = GetType().GetProperties(
            System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.DeclaredOnly);

        foreach (var property in properties)
        {
            var name = property.Name;
            var value = Convert.ToString(property.GetValue(this), Cultures.Default);

            yield return new(name, value ?? Question);
        }
    }

    protected static string MultiLines<T>(IEnumerable<T> values)
    {
        return string.Join(Environment.NewLine, values);
    }
}
