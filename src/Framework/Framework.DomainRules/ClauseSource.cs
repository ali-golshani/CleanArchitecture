namespace Framework.DomainRules;

public readonly struct ClauseSource(string name, object? value = null)
{
    public readonly string Name { get; } = name;
    public readonly object? Value { get; } = value;

    public static implicit operator ClauseSource(string name)
    {
        return new ClauseSource(name);
    }

    public static implicit operator ClauseSource((string Name, object Value) nameValue)
    {
        return new(nameValue.Name, nameValue.Value);
    }

    public override readonly string ToString()
    {
        if (Value is null)
        {
            return $"({Name})";
        }
        else
        {
            return $"({Name} , {Value})";
        }
    }
}
