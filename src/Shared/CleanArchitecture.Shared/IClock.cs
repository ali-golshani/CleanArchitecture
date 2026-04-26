namespace CleanArchitecture.Shared;

public interface IClock
{
    DateTime Now { get; }
    DateOnly Today { get; }
}
