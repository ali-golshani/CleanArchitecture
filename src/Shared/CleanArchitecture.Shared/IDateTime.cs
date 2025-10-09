namespace CleanArchitecture.Shared;

public interface IDateTime
{
    DateTime Now { get; }
    DateOnly Today { get; }
}
