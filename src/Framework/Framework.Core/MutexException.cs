namespace Framework.Core;

public class MutexException : Exception
{
    public MutexException(string? mutexName)
    {
        MutexName = mutexName;
    }

    public string? MutexName { get; }

    public override string Message => $"The specified named system mutex already existed";
}
