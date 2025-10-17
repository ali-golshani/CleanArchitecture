namespace Framework.Results;

public readonly struct ErrorSource(string name, object? value)
{
    public readonly string Name { get; } = name;
    public readonly object? Value { get; } = value;

    public static implicit operator ErrorSource(string sourceName)
    {
        return new(sourceName, null);
    }

    public static implicit operator ErrorSource((string Name, object Value) nameValue)
    {
        return new(nameValue.Name, nameValue.Value);
    }

    public override readonly string ToString()
    {
        if (Value == null)
        {
            return Name;
        }

        return $"({Name}, {Value})";
    }
}
