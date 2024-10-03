namespace Framework.Results;

public sealed class ErrorSource(string name, object? value)
{
    public string Name { get; } = name;
    public object? Value { get; } = value;

    public static implicit operator ErrorSource(string sourceName)
    {
        return new(sourceName, null);
    }

    public static implicit operator ErrorSource((string Name, object Value) nameValue)
    {
        return new(nameValue.Name, nameValue.Value);
    }

    public override string ToString()
    {
        if (Value == null)
        {
            return Name;
        }

        return $"({Name}, {Value})";
    }
}
