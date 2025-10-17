namespace Framework.Results;

public readonly record struct Empty
{
    public static readonly Empty Value = new();
    public static readonly Task<Empty> Task = System.Threading.Tasks.Task.FromResult(Value);
}
