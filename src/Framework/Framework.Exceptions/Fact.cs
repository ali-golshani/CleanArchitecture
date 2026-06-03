namespace Framework.Exceptions;

public readonly record struct Fact(string Name, object? Value)
{
    public static implicit operator Fact(string name)
    {
        return new Fact(name, null);
    }

    public static implicit operator Fact((string Name, object? Value) nameValue)
    {
        return new(nameValue.Name, nameValue.Value);
    }
    public override readonly string ToString()
    {
        return $"{{ {Name} = {Value} }}";
    }
}
